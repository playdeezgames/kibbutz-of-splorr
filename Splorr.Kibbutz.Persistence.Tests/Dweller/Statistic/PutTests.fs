module DwellerStatistic.PutTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Model

[<Test>]
let ``Put.It overrides the current value of a stastistic.`` () =
    GetTests.``Get.It returns the statistic when the given statistic does exist for the given dweller.``()
    DwellerStatisticStore.Put (Dummies.ValidDwellerIdentifier, Hunger, Some 1.0)
    let actual = DwellerStatisticStore.Get (Dummies.ValidDwellerIdentifier, Hunger)
    Assert.AreEqual(Some 1.0, actual)
