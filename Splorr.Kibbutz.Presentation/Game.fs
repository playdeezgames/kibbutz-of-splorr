namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

module Game = 

    type Command =
        | Quit

    let Load
            (context : CommonContext)
            : SessionIdentifier option =
        None

    let New
            (context: CommonContext)
            : SessionIdentifier =
        let session = Guid.NewGuid()
        Messages.Purge context session
        Messages.Put context (session, [Hued (Light Cyan, Line "Welcome to Kibbutz of SPLORR!!")])
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
        None

    type InvalidCommandSink = unit -> unit
    type HandleInvalidCommandContext =
        abstract member invalidCommandSink : InvalidCommandSink ref
    let private HandleInvalidCommand
            (context : CommonContext)
            : unit =
        (context :?> HandleInvalidCommandContext).invalidCommandSink.Value()


    let private RunLoop
            (context : CommonContext)
            (gamestate : SessionIdentifier)
            : SessionIdentifier option =
        UpdateDisplay context gamestate
        match PollForCommand context with
        | Some command ->
            HandleCommand context command gamestate
        | _ ->
            HandleInvalidCommand context 
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


