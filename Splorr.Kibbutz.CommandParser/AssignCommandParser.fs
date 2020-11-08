namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module AssignCommandParser =
    let private ParseAssignmentFromAssignmentName 
            (assignmentName : string)
            : Assignment option =
        match assignmentName with
        | "explore" ->
            Some Explore
        | "gather" ->
            Some Gather
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

    let internal Parse
            (context : CommonContext)
            (session : SessionIdentifier)
            (tokens : string list)
            : Command option =
        match tokens with
        | [ dwellerName; "to"; assignmentName ] ->
            ParseWellFormedAssignmentCommand context session (dwellerName, assignmentName)
        | _ ->
            None

