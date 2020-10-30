namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open System

module GameImplementation =
    let private ParseCommand
            (tokens : string list)
            : Game.Command option =
        match tokens with
        | [ "quit" ] ->
            Some Game.Command.Quit
        | _ ->
            tokens
            |> List.reduce
                (fun a b -> a + " " + b)
            |> Game.Command.Invalid 
            |> Some

    let internal PollForCommand() : Game.Command option =
        let oldColor = Console.ForegroundColor
        Console.ForegroundColor <- ConsoleColor.Gray
        Console.Write "\n>"
        let result = 
            Console.ReadLine().ToLower().Split(' ')
            |> Array.toList
            |> ParseCommand
        Console.ForegroundColor <- oldColor
        result

    let internal HandleInvalidCommand() : unit =
        ()


