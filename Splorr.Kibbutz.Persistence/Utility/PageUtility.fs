namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System

module internal PageUtility = 
    let private NoMoreThan
            (howMany : int)
            (log : 'T list)
            : 'T list =
        if log.Length >= howMany then
            log
            |> List.take howMany
        else
            log

    let private CalculateSkipForPageNumber (pageLength:int) (page: uint64) : int =
        if page=0UL then 
            0 
        else 
            ((page-1UL) |> int) * pageLength

    let internal GetPage
            (pageLength : int)
            (page : uint64)
            (itemList : 'T list option)
            : 'T list =
        let skipCount = CalculateSkipForPageNumber pageLength page
        match itemList with
        | Some items when items.Length>=skipCount ->
                items
                |> List.skip skipCount
                |> NoMoreThan pageLength
        | _ ->
            []

    let internal GetRecordCount
        (itemList : 'T list option)
        : int =
            itemList
            |> Option.map (fun x -> x.Length)
            |> Option.defaultValue 0

    let internal GetPageCount
        (pageLength : int)
        (itemList : 'T list option)
        : uint64 =
            ((GetRecordCount itemList)+pageLength-1) / (pageLength)
            |> uint64

