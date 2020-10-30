namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

module Game = 

    type Command =
        | Quit
        | Invalid of string

    let Load
            (context : CommonContext)
            : SessionIdentifier option =
        None

    let private Explain
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Put context (session, [Line ""])
        Messages.Put context (session, [Line "(the current state of the game will be explained here)"])

    let New
            (context: CommonContext)
            : SessionIdentifier =
        let session = Guid.NewGuid()
        Messages.Purge context session
        Messages.Put context (session, [Hued (Light Cyan, Line "Welcome to Kibbutz of SPLORR!!")])
        Explain context session
        session

    let private UpdateDisplay
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Get context session
        |> List.iter (Output.Write context)

    type CommandSource = unit -> Command option
    type PollForCommandContext =
        abstract member commandSource : CommandSource ref
    let private PollForCommand
            (context : CommonContext)
            : Command option =
        (context :?> PollForCommandContext).commandSource.Value()

    let private HandleCommand
            (context : CommonContext)
            (command : Command)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        match command with
        | Quit ->
            None
        | Invalid text ->
            Messages.Purge context session
            Messages.Put context (session, 
                [
                    Line ""
                    Hued (Red, Line (sprintf "I don't know what '%s' means." text))
                    Hued (Red, Line "Maybe you should try 'help'.")
                ])
            Explain context session
            Some session

    let private RunLoop
            (context : CommonContext)
            (gamestate : SessionIdentifier)
            : SessionIdentifier option =
        UpdateDisplay context gamestate
        match PollForCommand context with
        | Some command ->
            HandleCommand context command gamestate
        | _ ->
            Some gamestate

    let rec Run
            (context : CommonContext)
            (gamestate : SessionIdentifier)
            : unit =
        match RunLoop context gamestate with
        | Some nextState ->
            Run context nextState
        | None ->
            ()


