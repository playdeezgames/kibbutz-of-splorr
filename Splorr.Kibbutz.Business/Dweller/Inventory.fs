namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerInventory =
    let internal Inventory
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            (page : uint64)
            : Message =
        DwellerPageGenerator.Generate
            context
            session
            "inventory"
            (DwellerInventoryRepository.GetPageCount context)
            (DwellerInventoryRepository.GetPage context )
            DwellerExplainer.RenderInventoryAsMessage
            identifier
            page
    
