namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

module internal CommandHandler =
    let private HandleQuitCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        None

    let private HandleStandardCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            (messages : Message list)
            : SessionIdentifier option=
        Messages.Purge context session
        Messages.Put context session [Line ""]
        Messages.Put context session messages
        Explainer.Explain context session
        Some session

    let private HandleInvalidCommand
            (context : CommonContext)
            (invalidText : string)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        [
            Hued (Red, Line (sprintf "I don't know what '%s' means." invalidText))
            Hued (Red, Line "Maybe you should try 'help'.")
        ]
        |> HandleStandardCommand context session

    let private HandleHelpCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        [
            Hued (Yellow, Line ("(there will be helpful content here at some point, I assure you."))
        ]
        |> HandleStandardCommand context session

    let private HandleStartSettlementCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        Settlement.StartSettlementForSession context session
        |> HandleStandardCommand context session

    let internal HandleCommand
            (context : CommonContext)
            (command : Command)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        match command with
        | Invalid text ->
            HandleInvalidCommand context text session
        | Help ->
            HandleHelpCommand context session
        | Quit ->
            HandleQuitCommand context session
        | StartSettlement ->
            HandleStartSettlementCommand context session


