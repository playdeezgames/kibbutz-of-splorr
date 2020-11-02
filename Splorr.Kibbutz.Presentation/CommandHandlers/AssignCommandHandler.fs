namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module internal AssignCommandHandler =
    let internal Handle
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            (assignment : Assignment)
            : SessionIdentifier option =
        Dweller.Assign context session identifier assignment
        |> CommandHandlerUtility.HandleStandardCommand context session
