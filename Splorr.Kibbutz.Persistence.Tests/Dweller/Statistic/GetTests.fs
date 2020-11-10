module DwellerStatistic.GetTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Model

[<Test>]
let ``Get.It returns None when the given statistic does not exist for the given dweller.`` () =
    DwellerStatisticStore.Put (Dummies.ValidDwellerIdentifier, Hunger, None)
    let actual = DwellerStatisticStore.Get (Dummies.ValidDwellerIdentifier, Hunger)
    Assert.AreEqual(None, actual)

[<Test>]
let ``Get.It returns the statistic when the given statistic does exist for the given dweller.`` () =
    DwellerStatisticStore.Put (Dummies.ValidDwellerIdentifier, Hunger, Some 0.0)
    let actual = DwellerStatisticStore.Get (Dummies.ValidDwellerIdentifier, Hunger)
    Assert.AreEqual(Some 0.0, actual)
