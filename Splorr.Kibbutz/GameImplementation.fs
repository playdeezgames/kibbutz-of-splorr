namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module GameImplementation =
    let private fixedCommandTable : Map<string list, Command> =
        [
            [ "abandon" ], Command.AbandonSettlement
            [ "abandon"; "settlement" ], Command.AbandonSettlement
            [ "advance" ], Command.Advance
            [ "help" ], Command.Help
            [ "quit" ], Command.Quit
            [ "start" ], Command.StartSettlement
            [ "start"; "settlement" ], Command.StartSettlement
        ]
        |> Map.ofList

    let private ParseFixedCommand
            (tokens : string list)
            : Command option =
        fixedCommandTable
        |> Map.tryFind tokens

    let private ParseAssignmentFromAssignmentName 
            (assignmentName : string)
            : Assignment option =
        match assignmentName with
        | "explore" ->
            Some Explore
        | "rest" ->
            Some Rest
        | _ ->
            None

    let private ParseWellFormedAssignmentCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            (dwellerName : string, assignmentName : string)
            : Command option =
        ParseAssignmentFromAssignmentName assignmentName
        |> Option.bind
            (fun assignment -> 
                Dweller.FindIdentifierForName context session dwellerName
                |> Option.bind
                    (fun identifier ->
                        Assign (identifier, assignment)
                        |> Some))

    let private ParseAssignCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            (tokens : string list)
            : Command option =
        match tokens with
        | [ dwellerName; "to"; assignmentName ] ->
            ParseWellFormedAssignmentCommand context session (dwellerName, assignmentName)
        | _ ->
            None

    let private ParseCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            (tokens : string list)
            : Command option =
        match tokens with
        | "assign" :: tail ->
            ParseAssignCommand context session tail
        | _ ->
            ParseFixedCommand tokens
        |> Option.defaultValue
            (tokens
            |> List.reduce
                (fun a b -> a + " " + b)
            |> Command.Invalid)
        |> Some


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
            |> ParseCommand context session
        Console.ForegroundColor <- oldColor
        result


