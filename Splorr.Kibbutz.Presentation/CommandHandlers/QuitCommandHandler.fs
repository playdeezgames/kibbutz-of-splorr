namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module internal QuitCommandHandler =
    let internal Handle
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        if Settlement.HasSettlementForSession context session then
            [
                Hued (Red, Line "You cannot quit without first abandoning the settlement.")
            ]
            |> Group
            |> CommandHandlerUtility.HandleStandardCommand context session
        else
            None