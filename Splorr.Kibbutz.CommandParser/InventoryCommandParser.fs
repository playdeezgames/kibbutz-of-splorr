namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module InventoryCommandParser =
    let private ParseDwellerName
            (context : CommonContext)
            (session : SessionIdentifier)
            (dwellerName: string, page : uint64)
            : Command option =
        Dweller.FindIdentifierForName context session dwellerName
        |> Option.map
            (fun identifier ->
                (identifier, page) |> Inventory)

    let private ParseDwellerNameAndPageNumber
            (context : CommonContext)
            (session : SessionIdentifier)
            (dwellerName: string, pageNumber : string)
            : Command option =
        match System.UInt64.TryParse pageNumber with
        | true, page ->
            ParseDwellerName context session (dwellerName, page)
        | _ ->
            None

    let internal Parse
            (context : CommonContext)
            (session : SessionIdentifier)
            (tokens : string list)
            : Command option =
        match tokens with
        | [ dwellerName ] ->
            Some (dwellerName, "1")
        | [ dwellerName; pageNumber ] ->
            Some (dwellerName, pageNumber)
        | [ dwellerName; "page"; pageNumber] ->
            Some (dwellerName, pageNumber)
        | _ -> 
            None
        |> Option.bind (ParseDwellerNameAndPageNumber context session)


