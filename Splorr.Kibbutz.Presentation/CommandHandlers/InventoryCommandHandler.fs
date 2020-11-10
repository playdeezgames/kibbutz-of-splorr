namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module InventoryCommandHandler =
    let internal Handle 
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            (page : uint64)
            : SessionIdentifier option =
        Dweller.Inventory context session identifier page
        |> CommandHandlerUtility.HandleStandardCommand context session


