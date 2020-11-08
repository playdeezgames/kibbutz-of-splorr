namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System

module DwellerLogStore =
    let private PageHistoryLength = 10
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

    let private BriefHistoryLength = 3

    let private NoMoreThan
            (howMany : int)
            (log : (TurnCounter * Message) list)
            : (TurnCounter * Message) list =
        if log.Length >= howMany then
            log
            |> List.take howMany
        else
            log

    let GetBriefHistory
            (identifier : DwellerIdentifier)
            : (TurnCounter * Message) list =
        match dwellerLogs.Value |> Map.tryFind identifier with
        | Some log ->
            log
            |> NoMoreThan BriefHistoryLength
        | None ->
            []

    let private CalculateSkipForPageNumber (page: uint64) : int =
        if page=0UL then 
            0 
        else 
            ((page-1UL) |> int) * PageHistoryLength

    let GetPageHistory
            (identifier : DwellerIdentifier, page : uint64)
            : (TurnCounter * Message) list =
        let skipCount = CalculateSkipForPageNumber page
        match dwellerLogs.Value |> Map.tryFind identifier with
        | Some log when log.Length>=skipCount ->
                log
                |> List.skip skipCount
                |> NoMoreThan PageHistoryLength
        | _ ->
            []

    let private GetRecordCount
            (identifier : DwellerIdentifier)
            : int =
        dwellerLogs.Value 
        |> Map.tryFind identifier
        |> Option.map (fun x -> x.Length)
        |> Option.defaultValue 0

    let GetHistoryPageCount
            (identifier : DwellerIdentifier)
            : uint64 =
        ((GetRecordCount identifier)+PageHistoryLength-1) / (PageHistoryLength)
        |> uint64
