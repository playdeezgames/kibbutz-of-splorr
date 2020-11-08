namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerHistory =
    let private UnknownDwellerHistoryMessages =
        [
            Line "Unknown dweller."
        ]
        |> Group

    let private NoHistoryForDwellerMessages =
        [
            Line "No history for dweller."
        ]
        |> Group

    let private HistoryForKnownDweller
            (context : CommonContext)
            (identifier : DwellerIdentifier)
            (page : uint64)
            : Message =
        let page = if page=0UL then 1UL else page
        let pages = DwellerLogRepository.GetHistoryPageCount context identifier
        let dwellerLogMessages =
            DwellerLogRepository.GetPageHistory context (identifier, page)
            |> List.map DwellerExplainer.RenderHistoryAsMessage
        match dwellerLogMessages with
        | [] ->
            NoHistoryForDwellerMessages
        | history ->
            Group
                [
                    (page, pages) ||> sprintf "Page %u of %u:" |> Line
                    Group dwellerLogMessages
                ]

    let internal History
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            (page : uint64)
            : Message =
        if DwellerSession.ExistsForSession context session identifier then
            HistoryForKnownDweller context identifier page
        else
            UnknownDwellerHistoryMessages


