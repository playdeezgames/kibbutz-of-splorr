module DwellerStatistic.PurgeTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Model

[<Test>]
let ``Purge.It removes all statistics for a dweller.`` () =
    let statistics =
        [ Hunger; Fatigue ]
    statistics
    |> List.iter (fun statistic -> DwellerStatisticStore.Put (Dummies.ValidDwellerIdentifier, statistic, Some 1.0))
    DwellerStatisticStore.Purge Dummies.ValidDwellerIdentifier
    statistics
    |> List.iter (fun statistic ->
        let actual = DwellerStatisticStore.Get (Dummies.ValidDwellerIdentifier, statistic)
        Assert.AreEqual(None, actual))

