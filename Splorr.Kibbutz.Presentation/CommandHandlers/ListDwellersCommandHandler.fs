namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module internal ListDwellersCommandHandler =
    let internal Handle
            (context : CommonContext)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        Settlement.ListDwellers context session
        |> CommandHandlerUtility.HandleStandardCommand context session


