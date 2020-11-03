namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module CommandParser =
    let internal Parse
            (context : CommonContext)
            (session : SessionIdentifier)
            (tokens : string list)
            : Command option =
        match tokens with
        | "assign" :: tail ->
            AssignCommandParser.Parse context session tail
        | _ ->
            FixedCommandParser.Parse tokens
        |> Option.defaultValue
            (tokens
            |> List.reduce
                (fun a b -> a + " " + b)
            |> Command.Invalid)
        |> Some
