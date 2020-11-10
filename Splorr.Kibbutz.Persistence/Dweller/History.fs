namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System

module DwellerHistoryStore =
    let private PageHistoryLength = 10
    let private BriefHistoryLength = 3
    let private dwellerLogs : Map<DwellerIdentifier, (TurnCounter * Message) list> ref = ref Map.empty

    let LogForDweller
            (identifier: DwellerIdentifier,
                turn: TurnCounter,
                message: Message)
            : unit =
        let updatedLog = 
            match dwellerLogs.Value |> Map.tryFind identifier with
            | Some log ->
                List.append [(turn, message)] log
            | None ->
                [(turn, message)]
        dwellerLogs :=
            dwellerLogs.Value
            |> Map.add identifier updatedLog

    let PurgeLogsForDweller
            (identifier : DwellerIdentifier)
            : unit =
        dwellerLogs :=
            dwellerLogs.Value
            |> Map.remove identifier

    let GetBriefHistory
            (identifier : DwellerIdentifier)
            : (TurnCounter * Message) list =
        dwellerLogs.Value 
        |> Map.tryFind identifier
        |> PageUtility.GetPage BriefHistoryLength 0UL

    let GetPageHistory
            (identifier : DwellerIdentifier, page : uint64)
            : (TurnCounter * Message) list =
        dwellerLogs.Value 
        |> Map.tryFind identifier
        |> PageUtility.GetPage PageHistoryLength page

    let GetHistoryPageCount
            (identifier : DwellerIdentifier)
            : uint64 =
        dwellerLogs.Value 
        |> Map.tryFind identifier
        |> PageUtility.GetPageCount PageHistoryLength
