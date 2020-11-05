﻿namespace Splorr.Kibbutz

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
        | "assign" :: tail ->
            AssignCommandParser.Parse context session tail
        | [ "dweller"; dwellerName ] ->
            DwellerCommandParser.Parse context session dwellerName
        | _ ->
            FixedCommandParser.Parse tokens
        |> Option.defaultValue
            (tokens
            |> List.reduce
                (fun a b -> a + " " + b)
            |> Command.Invalid)
        |> Some