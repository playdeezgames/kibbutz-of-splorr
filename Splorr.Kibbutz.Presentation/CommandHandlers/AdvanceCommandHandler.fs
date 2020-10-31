namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System

module internal AdvanceCommandHandler =
    let internal Handle
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        Settlement.Advance context session
        |> CommandHandlerUtility.HandleStandardCommand context session
