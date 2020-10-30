namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

type Command =
    | Quit
    | Invalid of string

module internal CommandHandler =
    let internal HandleCommand
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
            Explainer.Explain context session
            Some session


