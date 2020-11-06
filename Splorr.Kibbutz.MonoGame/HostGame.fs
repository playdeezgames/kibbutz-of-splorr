﻿namespace Splorr.Kibbutz.Monogame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
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

    let ParseCommand () : Command option =
        keybuffer.Value.ToLower().Split(' ') 
        |> Array.toList 
        |> CommandParser.Parse context session.Value.Value

    let DispatchCommand (command:Command) : unit =
        GameImplementation.commandQueue := List.append GameImplementation.commandQueue.Value [ command ]
        UpdateScreen()

    let HandleNewLine() : unit =
        OutputImplementation.Write (Message.Line "")
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

    let HandleKeyDown (args:InputKeyEventArgs) : unit =
        //TODO - for handling Function keys!
        ()

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


