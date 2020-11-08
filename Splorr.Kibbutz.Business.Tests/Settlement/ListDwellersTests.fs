module Settlement.ListDwellersTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Kibbutz.Model

[<Test>]
let ``ListDwellers.It lists dwellers for the settlement.`` () =
    let calledGetDwellerList = ref false
    let callsForGetDweller = ref 0UL
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource := Spies.SourceTable(callsForGetDweller, Dummies.ValidDwellerTable)
    let actual = Settlement.ListDwellers context Dummies.ValidSessionIdentifier
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 5)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.AreEqual(3UL, callsForGetDweller.Value)

