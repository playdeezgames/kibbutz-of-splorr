namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System

module internal HelpCommandHandler =
    let internal Handle
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        [
            Hued (Yellow, Line ("abandon - abandons a settlement"))
            Hued (Yellow, Line ("advance - advances a settlement by one turn"))
            Hued (Yellow, Line ("help - shows help"))
            Hued (Yellow, Line ("start - starts a settlement"))
            Hued (Yellow, Line ("quit - quits the game"))
        ]
        |> CommandHandlerUtility.HandleStandardCommand context session
