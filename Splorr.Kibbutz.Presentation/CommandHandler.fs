namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

type Command =
    | Quit
    | Invalid of string

module internal CommandHandler =
    let private HandleQuitCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        None

    let private HandleInvalidCommand
            (context : CommonContext)
            (invalidText : string)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        Messages.Purge context session
        Messages.Put context (session, 
            [
                Line ""
                Hued (Red, Line (sprintf "I don't know what '%s' means." invalidText))
                Hued (Red, Line "Maybe you should try 'help'.")
            ])
        Explainer.Explain context session
        Some session

    let internal HandleCommand
            (context : CommonContext)
            (command : Command)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        match command with
        | Quit ->
            HandleQuitCommand context session
        | Invalid text ->
            HandleInvalidCommand context text session


