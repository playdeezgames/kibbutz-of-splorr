namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System

module DwellerLogStore =
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

    let GetBriefHistory
            (identifier : DwellerIdentifier)
            : (TurnCounter * Message) list =
        match dwellerLogs.Value |> Map.tryFind identifier with
        | Some log ->
            if log.Length > BriefHistoryLength then
                log
                |> List.take BriefHistoryLength
            else
                log
        | None ->
            []

    let GetPageHistory
            (identifier : DwellerIdentifier, page : uint64)
            : (TurnCounter * Message) list =
        raise (NotImplementedException "NO UNIT TESTS")