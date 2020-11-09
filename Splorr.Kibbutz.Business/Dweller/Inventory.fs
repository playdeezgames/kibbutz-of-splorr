namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerInventory =
    let private UnknownDwellerInventoryMessages =
        [
            Line "Unknown dweller."
        ]
        |> Group

    let private NoInventoryForDwellerMessages =
        [
            Line "No inventory for dweller."
        ]
        |> Group

    let private InventoryForKnownDweller
            (context : CommonContext)
            (identifier : DwellerIdentifier)
            (page : uint64)
            : Message =
        let page = if page=0UL then 1UL else page
        let pages = DwellerInventoryRepository.GetPageCount context identifier
        let dwellerLogMessages =
            DwellerInventoryRepository.GetPage context (identifier, page)
            |> List.map DwellerExplainer.RenderInventoryAsMessage
        match dwellerLogMessages with
        | [] ->
            NoInventoryForDwellerMessages
        | history ->
            Group
                [
                    (page, pages) ||> sprintf "Page %u of %u:" |> Line
                    Group dwellerLogMessages
                ]

    let internal Inventory
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            (page : uint64)
            : Message =
        if DwellerSession.ExistsForSession context session identifier then
            InventoryForKnownDweller context identifier page
        else
            UnknownDwellerInventoryMessages


