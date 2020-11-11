namespace Splorr.Kibbutz.Monogame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open System.IO
open System
open Splorr.Common
open Splorr.Kibbutz.Model
open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz

type HostGame(context : CommonContext) as this =
    inherit Game()

    do
        this.Content.RootDirectory <- "Content"

    let context = context
    let session : SessionIdentifier option ref = ref None
    let graphics = new GraphicsDeviceManager(this)
    let spriteBatch : SpriteBatch ref = ref null
    let texture : Texture2D ref = ref null
    let keybuffer : string ref = ref ""
    let lastKeyBuffer : string ref = ref ""

    let textureColumns = 16
    let textureCellWidth = 16
    let textureCellHeight = 16
    let sources : Map<byte, Nullable<Rectangle>> =
        [0uy..255uy]
        |> List.map
            (fun character ->
                (character, 
                    Nullable<Rectangle>
                        (Rectangle
                            (((character |> int) % textureColumns) * textureCellWidth, 
                            ((character |> int) / textureColumns)* textureCellHeight, 
                            textureCellWidth, 
                            textureCellHeight))))
        |> Map.ofList

    let WritePromptToScreen() : unit =
        OutputImplementation.Write (Message.Line "")
        OutputImplementation.Write (Message.Text ">")

    let UpdateScreen () : unit =
        session := Game.RunSlice context session.Value.Value
        if session.Value.IsSome then
            WritePromptToScreen()
        else
            this.Exit()

    let hotKeys : Map<Keys, string * Command> ref=
        ref
            ([
                Keys.F1, ("help", Command.Help)
                Keys.F2, ("advance", Command.Advance)
            ]
            |> Map.ofList)

    let AttemptSettingHotkey
            (tokens : string list)
            (key : Keys)
            : bool =
        match tokens |> CommandParser.Parse context session.Value.Value with
        | None ->
            false
        | Some (Invalid _) ->
            OutputImplementation.Write (Message.Line "")
            OutputImplementation.Write (Message.Hued (Red, Message.Line "That hotkey command did not parse!"))
            false
        | Some command ->
            let commandText = tokens |> List.reduce (fun a b -> a + " " + b)
            OutputImplementation.Write (Message.Line "")
            OutputImplementation.Write (Message.Hued (Green, Message.Line (sprintf "Set hotkey %s to '%s'" (key.ToString()) commandText)))
            hotKeys:= 
                hotKeys.Value 
                |> Map.add key ((commandText, command))
            true

    let keyMap : Map<string, Keys> =
        [
            "f1", Keys.F1
            "f2", Keys.F2
            "f3", Keys.F3
            "f4", Keys.F4
            "f5", Keys.F5
            "f6", Keys.F6
            "f7", Keys.F7
            "f8", Keys.F8
            "f9", Keys.F9
            "f10", Keys.F10
            "f11", Keys.F11
            "f12", Keys.F12
        ]
        |> Map.ofList

    let PreparseHotkey
            (tokens : string list)
            : bool =
        match tokens with
        | keyName :: tail ->
            keyMap
            |> Map.tryFind keyName
            |> Option.map
                (AttemptSettingHotkey tail)
            |> Option.defaultValue false
        | _ ->
            false

    let PreparseCommand 
            (tokens : string list)
            : bool =
        match tokens with
        | "hotkey" :: tail ->
            PreparseHotkey tail
        | _ ->
            false

    let ParseCommand () : Command option =
        let tokens = 
            keybuffer.Value.ToLower().Split(' ') 
            |> Array.toList 
        if PreparseCommand tokens then
            OutputImplementation.Write (Message.Line "")
            UpdateScreen()
            None
        else
            tokens
            |> CommandParser.Parse context session.Value.Value

    let DispatchCommand (command:Command) : unit =
        GameImplementation.commandQueue := List.append GameImplementation.commandQueue.Value [ command ]
        UpdateScreen()

    let HandleNewLine() : unit =
        OutputImplementation.Write (Message.Line "")
        lastKeyBuffer := keybuffer.Value
        ParseCommand()
        |> Option.iter DispatchCommand
        keybuffer := ""

    let DoNothing = ignore

    let HandleBackspace() : unit =
        if keybuffer.Value.Length>0 then
            keybuffer := keybuffer.Value.Substring(0, keybuffer.Value.Length - 1)
            OutputImplementation.Backspace()

    let AddToKeyBuffer(s:string) : unit =
        keybuffer := keybuffer.Value + (s)
        OutputImplementation.Write (Message.Text (s))

    let HandleTextInput (args: TextInputEventArgs) : unit =
        match args.Character with
        | '\r' ->
            HandleNewLine()

        | '\t' //tab
        | '\u007f' //delete
        | '\u001b' -> //escape
            DoNothing()

        | '\b' ->
            HandleBackspace()

        | c ->
            c.ToString()
            |> AddToKeyBuffer

    let ResetKeyBuffer() : unit =
        while keybuffer.Value.Length>0 do
            HandleBackspace()

    let HandleQuickCommand (text:string) (command:Command) : unit =
        ResetKeyBuffer()
        lastKeyBuffer:= text
        OutputImplementation.Write (Message.Line text)
        DispatchCommand command
        keybuffer := ""

    let HandleKeyDown (args:InputKeyEventArgs) : unit =
        match hotKeys.Value.TryFind args.Key with
        | Some (text, command) ->
            HandleQuickCommand text command
        | None ->
            if args.Key = Keys.Up then
                ResetKeyBuffer()
                OutputImplementation.Write (Message.Line lastKeyBuffer.Value)
                keybuffer := lastKeyBuffer.Value

    let InitializeGraphics() : unit =
        graphics.PreferredBackBufferWidth <- OutputImplementation.screenWidth
        graphics.PreferredBackBufferHeight <- OutputImplementation.screenHeight
        graphics.ApplyChanges()

    let InitializeGame() : unit =
        session := 
            Game.Load context
            |> Option.defaultValue (Game.New context)
            |> Some
        UpdateScreen()

    let RenderCell 
            (position:int) 
            (cell:TextCell) 
            : unit =
        OutputImplementation.destinations
        |> Map.tryFind position
        |> Option.iter
            (fun destination ->
                spriteBatch.Value.Draw(texture.Value, destination, sources.[cell.character], cell.color))

    let RenderScreen() : unit =
        spriteBatch.Value.Begin()
        OutputImplementation.buffer.Value
        |> Map.iter RenderCell
        spriteBatch.Value.End()

    member private this.InitializeWindow() =
        this.Window.Title <- "Kibbutz of SPLORR!!"
        this.Window.TextInput.Add(HandleTextInput)
        this.Window.KeyDown.Add(HandleKeyDown)

    override this.Initialize() =
        this.InitializeWindow()
        InitializeGraphics()

        spriteBatch := new SpriteBatch(this.GraphicsDevice)
        base.Initialize()

        InitializeGame()

    override this.LoadContent() =
        texture := Texture2D.FromStream(this.GraphicsDevice, new FileStream("Content/romfont8x8.png", FileMode.Open))
    
    override this.Update (delta:GameTime)  =
        DoNothing()

    override this.Draw (delta:GameTime) =
        Color.Black
        |>  this.GraphicsDevice.Clear
        RenderScreen()


