namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System

module internal StartSettlementCommandHandler =
    let internal Handle
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        Settlement.StartSettlementForSession context session
        |> CommandHandlerUtility.HandleStandardCommand context session
