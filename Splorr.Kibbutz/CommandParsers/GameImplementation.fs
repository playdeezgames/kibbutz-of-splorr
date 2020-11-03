namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module GameImplementation =
    let internal PollForCommand
            (context : CommonContext,
                session : SessionIdentifier)
            : Command option =
        let oldColor = Console.ForegroundColor
        Console.ForegroundColor <- ConsoleColor.Gray
        Console.Write "\n>"
        let result = 
            Console.ReadLine().ToLower().Split(' ')
            |> Array.toList
            |> CommandParser.Parse context session
        Console.ForegroundColor <- oldColor
        result


