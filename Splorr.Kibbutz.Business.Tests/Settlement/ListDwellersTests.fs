module Settlement.ListDwellersTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``ListDwellers.It lists dwellers for the settlement.`` () =
    let calledGetDwellerList = ref false
    let callsForGetDweller = ref 0UL
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource := Spies.SourceTable(callsForGetDweller, Dummies.ValidDwellerTable)
    let actual = Settlement.ListDwellers context Dummies.ValidSessionIdentifier
    Assert.AreEqual(5, actual.Length)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.AreEqual(3UL, callsForGetDweller.Value)

