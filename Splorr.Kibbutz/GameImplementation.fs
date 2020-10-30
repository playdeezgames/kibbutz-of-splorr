namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open System

module GameImplementation =
    let private fixedCommandTable : Map<string list, Command> =
        [
            [ "abandon" ], Command.AbandonSettlement
            [ "abandon"; "settlement" ], Command.AbandonSettlement
            [ "help" ], Command.Help
            [ "quit" ], Command.Quit
            [ "start" ], Command.StartSettlement
            [ "start"; "settlement" ], Command.StartSettlement
        ]
        |> Map.ofList

    let private ParseCommand
            (tokens : string list)
            : Command option =
        fixedCommandTable
        |> Map.tryFind tokens
        |> Option.defaultValue
            (tokens
            |> List.reduce
                (fun a b -> a + " " + b)
            |> Command.Invalid)
        |> Some

    let internal PollForCommand() : Command option =
        let oldColor = Console.ForegroundColor
        Console.ForegroundColor <- ConsoleColor.Gray
        Console.Write "\n>"
        let result = 
            Console.ReadLine().ToLower().Split(' ')
            |> Array.toList
            |> ParseCommand
        Console.ForegroundColor <- oldColor
        result


