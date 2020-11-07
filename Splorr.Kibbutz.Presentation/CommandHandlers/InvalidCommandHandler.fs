namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module internal InvalidCommandHandler =
    let internal Handle
            (context : CommonContext)
            (invalidText : string)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        [
            Hued (Red, Line (sprintf "I don't know what '%s' means." invalidText))
            Hued (Red, Line "Maybe you should try 'help'.")
        ]
        |> Group
        |> CommandHandlerUtility.HandleStandardCommand context session
