namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open System

module SettlementImplementation = 
    let private settlements : Map<SessionIdentifier, Settlement> ref = ref Map.empty

    let internal GetSettlementForSession
            (session : SessionIdentifier)
            : Settlement option =
        settlements.Value
        |> Map.tryFind session

    let internal PutSettlementForSession
            (session : SessionIdentifier,
                settlement : Settlement option)
            : unit =
        match settlement with
        | Some s ->
            settlements :=
                settlements.Value |> Map.add session s
        | None ->
            settlements :=
                settlements.Value |> Map.remove session

