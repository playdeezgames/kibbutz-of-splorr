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
            (key : Keys)
            (tokens : string list)
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

    let PreparseHotkey
            (tokens : string list)
            : bool =
        match tokens with
        | "f1" :: tail ->
            AttemptSettingHotkey Keys.F1 tail
        | "f2" :: tail ->
            AttemptSettingHotkey Keys.F2 tail
        | "f3" :: tail ->
            AttemptSettingHotkey Keys.F3 tail
        | "f4" :: tail ->
            AttemptSettingHotkey Keys.F4 tail
        | "f5" :: tail ->
            AttemptSettingHotkey Keys.F5 tail
        | "f6" :: tail ->
            AttemptSettingHotkey Keys.F6 tail
        | "f7" :: tail ->
            AttemptSettingHotkey Keys.F7 tail
        | "f8" :: tail ->
            AttemptSettingHotkey Keys.F8 tail
        | "f9" :: tail ->
            AttemptSettingHotkey Keys.F9 tail
        | "f10" :: tail ->
            AttemptSettingHotkey Keys.F10 tail
        | "f11" :: tail ->
            AttemptSettingHotkey Keys.F11 tail
        | "f12" :: tail ->
            AttemptSettingHotkey Keys.F12 tail
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


