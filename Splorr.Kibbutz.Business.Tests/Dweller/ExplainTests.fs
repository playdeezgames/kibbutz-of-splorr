module Dweller.ExplainTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Kibbutz.Model

[<Test>]
let ``Explain.It returns messages explaining a dweller when that dweller exists.`` () =
    let callsForGetDweller = ref 0UL
    let calledGetDwellerList = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource := Spies.SourceTable(callsForGetDweller, Dummies.ValidDwellerTable)
    let actual = Dweller.Explain context Dummies.ValidSessionIdentifier Dummies.ValidDwellerIdentifier
    Assert.AreEqual(4, actual.Length)
    Assert.AreEqual(1UL, callsForGetDweller.Value)
    Assert.IsTrue(calledGetDwellerList.Value)


[<Test>]
let ``Explain.It returns empty list when that dweller does not exist for the given session.`` () =
    let calledGetDwellerList = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, [])
    let actual = Dweller.Explain context Dummies.ValidSessionIdentifier Dummies.ValidDwellerIdentifier
    Assert.AreEqual(0, actual.Length)
    Assert.IsTrue(calledGetDwellerList.Value)
