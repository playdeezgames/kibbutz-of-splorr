namespace Splorr.Kibbutz.Monogame

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module GameImplementation =
    let internal commandQueue : Command list ref = ref []

    let internal PollForCommand
            (context : CommonContext,
                session : SessionIdentifier)
            : Command option =
        match commandQueue.Value |> List.tryHead with
        | Some item ->
            commandQueue := commandQueue.Value |> List.skip 1
            Some item
        | None ->
            None


