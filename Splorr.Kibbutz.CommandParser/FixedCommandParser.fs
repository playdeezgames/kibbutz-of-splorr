namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module FixedCommandParser =
    let private fixedCommandTable : Map<string list, Command> =
        [
            [ "abandon" ], Command.AbandonSettlement
            [ "abandon"; "settlement" ], Command.AbandonSettlement
            [ "advance" ], Command.Advance
            [ "dwellers" ], Command.ListDwellers
            [ "help" ], Command.Help
            [ "quit" ], Command.Quit
            [ "start" ], Command.StartSettlement
            [ "start"; "settlement" ], Command.StartSettlement
        ]
        |> Map.ofList

    let internal Parse
            (tokens : string list)
            : Command option =
        fixedCommandTable
        |> Map.tryFind tokens
