namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open System

module SettlementImplementation = 
    let private settlements : Map<SessionIdentifier, Settlement> ref = ref Map.empty

    let GetSettlementForSession
            (session : SessionIdentifier)
            : Settlement option =
        settlements.Value
        |> Map.tryFind session

    let PutSettlementForSession
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

