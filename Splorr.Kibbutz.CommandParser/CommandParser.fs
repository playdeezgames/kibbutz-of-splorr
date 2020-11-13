namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module CommandParser =
    let Parse
            (context : CommonContext)
            (session : SessionIdentifier)
            (tokens : string list)
            : Command option =
        match tokens with
        | [ "area"; x; y ] ->
            AreaCommandParser.Parse context session x y
        | "assign" :: tail ->
            AssignCommandParser.Parse context session tail
        | [ "dweller"; dwellerName ] ->
            DwellerCommandParser.Parse context session dwellerName
        | "history" :: tail ->
            HistoryCommandParser.Parse context session tail
        | "inventory" :: tail ->
            InventoryCommandParser.Parse context session tail
        | _ ->
            FixedCommandParser.Parse tokens
        |> Option.defaultValue
            (tokens
            |> List.reduce
                (fun a b -> a + " " + b)
            |> Command.Invalid)
        |> Some
