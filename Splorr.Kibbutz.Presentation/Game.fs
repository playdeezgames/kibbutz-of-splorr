namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module Game = 

    let Load
            (context : CommonContext)
            : SessionIdentifier option =
        None

    let New
            (context: CommonContext)
            : SessionIdentifier =
        let session = Session.GenerateIdentifier context
        Messages.Purge context session
        Messages.Put context session [Hued (Light Cyan, Line "Welcome to Kibbutz of SPLORR!!")]
        Settlement.Explain context session
        session

    let private UpdateDisplay
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Get context session
        |> List.iter (Output.Write context)

    type CommandSource = CommonContext * SessionIdentifier -> Command option
    type PollForCommandContext =
        abstract member commandSource : CommandSource ref
    let private PollForCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            : Command option =
        (context :?> PollForCommandContext).commandSource.Value (context, session)

    let private RunLoop
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        UpdateDisplay context session
        match PollForCommand context session with
        | Some command ->
            CommandHandler.HandleCommand context command session
        | _ ->
            Some session

    let rec Run
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        match RunLoop context session with
        | Some nextState ->
            Run context nextState
        | None ->
            ()


