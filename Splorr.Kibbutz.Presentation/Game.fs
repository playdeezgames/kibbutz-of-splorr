namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System

module Game = 

    let Load
            (context : CommonContext)
            : SessionIdentifier option =
        None

    let New
            (context: CommonContext)
            : SessionIdentifier =
        let session = Guid.NewGuid()
        Messages.Purge context session
        Messages.Put context session [Hued (Light Cyan, Line "Welcome to Kibbutz of SPLORR!!")]
        Explainer.Explain context session
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

    let private RunLoop
            (context : CommonContext)
            (gamestate : SessionIdentifier)
            : SessionIdentifier option =
        UpdateDisplay context gamestate
        match PollForCommand context with
        | Some command ->
            CommandHandler.HandleCommand context command gamestate
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


