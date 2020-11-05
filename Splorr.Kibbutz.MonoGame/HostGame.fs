namespace Splorr.Kibbutz.Monogame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System.IO
open System
open Splorr.Common

type HostGame(context : CommonContext) as this =
    inherit Game()

    do
        this.Content.RootDirectory <- "Content"

    let context = context
    let session : Splorr.Kibbutz.Model.SessionIdentifier option ref = 
        ref 
            (Some (Splorr.Kibbutz.Presentation.Game.Load context
            |> Option.defaultValue (Splorr.Kibbutz.Presentation.Game.New context)))
    let graphics = new GraphicsDeviceManager(this)
    let spriteBatch : SpriteBatch ref = ref null
    let texture : Texture2D ref = ref null
    let blankTexture : Texture2D ref = ref null
    let keybuffer : string ref = ref ""

    let UpdateScreen () : unit =
        session := Splorr.Kibbutz.Presentation.Game.RunSlice context session.Value.Value
        if session.Value.IsSome then
            OutputImplementation.Write (Splorr.Kibbutz.Model.Message.Line "")
            OutputImplementation.Write (Splorr.Kibbutz.Model.Message.Text ">")
        else
            this.Exit()

    let HandleTextInput (args: TextInputEventArgs) : unit =
        match args.Character with
        | '\r' ->
            OutputImplementation.Write (Splorr.Kibbutz.Model.Message.Line "")
            keybuffer.Value.ToLower().Split(' ') 
            |> Array.toList 
            |> Splorr.Kibbutz.CommandParser.Parse context session.Value.Value
            |> Option.iter
                (fun command ->
                    GameImplementation.commandQueue := List.append GameImplementation.commandQueue.Value [ command ]
                    UpdateScreen())
            keybuffer := ""
        | '\t'
        | '\u007f'
        | '\u001b' ->
            ()
        | '\b' ->
            if keybuffer.Value.Length>0 then
                keybuffer := keybuffer.Value.Substring(0, keybuffer.Value.Length - 1)
                OutputImplementation.Backspace()
        | c ->
            keybuffer := keybuffer.Value + (c.ToString())
            OutputImplementation.Write (Splorr.Kibbutz.Model.Message.Text (args.Character.ToString()))

    let HandleKeyDown (args:InputKeyEventArgs) : unit =
        ()

    override this.Initialize() =
        this.Window.Title <- "Kibbutz of SPLORR!!"
        graphics.PreferredBackBufferWidth <- 1280
        graphics.PreferredBackBufferHeight <- 720
        this.Window.TextInput.Add(HandleTextInput)
        this.Window.KeyDown.Add(HandleKeyDown)
        graphics.ApplyChanges()
        spriteBatch := new SpriteBatch(this.GraphicsDevice)
        UpdateScreen()
        base.Initialize()

    override this.LoadContent() =
        texture := Texture2D.FromStream(this.GraphicsDevice, new FileStream("Content/romfont8x8.png", FileMode.Open))
        blankTexture := Texture2D.FromStream(this.GraphicsDevice, new FileStream("Content/blank.png", FileMode.Open))
    
    override this.Update (delta:GameTime)  =
        ()

    override this.Draw (delta:GameTime) =
        Color.Black
        |>  this.GraphicsDevice.Clear
        spriteBatch.Value.Begin()
        OutputImplementation.buffer.Value
        |> Map.iter
            (fun position cell ->
                let destination = Rectangle((position % OutputImplementation.screenColumns)*16,(position / OutputImplementation.screenColumns)*16,16,16)
                spriteBatch.Value.Draw(texture.Value, destination, Nullable<Rectangle>(Rectangle(((cell.character |> int) % 16) * 16, ((cell.character |> int) / 16)* 16, 16, 16)), cell.color))

        spriteBatch.Value.End()


