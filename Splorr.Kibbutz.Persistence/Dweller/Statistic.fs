namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System

module DwellerStatisticStore =
    let private store : Map<DwellerIdentifier * DwellerStatisticIdentifier, float> ref = ref Map.empty

    let Get
            (identifier: DwellerIdentifier, statistic: DwellerStatisticIdentifier)
            : DwellerStatistic option =
        store.Value
        |> Map.tryFind (identifier, statistic)

    let Put
            (identifier: DwellerIdentifier, statistic: DwellerStatisticIdentifier, value : DwellerStatistic option)
            : unit =
        match value with
        | Some v ->
            store := 
                store.Value
                |> Map.add (identifier, statistic) v
        | None ->
            store :=
                store.Value
                |> Map.remove (identifier, statistic)

