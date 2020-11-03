namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module internal ExplainDwellerCommandHandler =
    let internal Handle 
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : SessionIdentifier option =
        Dweller.Explain context session identifier
        |> CommandHandlerUtility.HandleStandardCommand context session

