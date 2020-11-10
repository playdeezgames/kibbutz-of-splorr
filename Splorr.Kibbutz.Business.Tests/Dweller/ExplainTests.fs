module Dweller.ExplainTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Kibbutz.Model

[<Test>]
let ``Explain.It returns messages explaining a dweller when that dweller exists.`` () =
    let callsForGetDweller = ref 0UL
    let calledGetDwellerList = ref false
    let callsForBriefHistory = ref 0UL
    let callsForGetStatistic = ref 0UL
    let context = Contexts.TestContext()
    (context :> DwellerStatisticRepository.GetContext).dwellerStatisticSource := Spies.SourceCounter(callsForGetStatistic, None)
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource := Spies.SourceTable(callsForGetDweller, Dummies.ValidDwellerTable)
    (context :> DwellerHistoryRepository.GetBriefHistoryContext).dwellerBriefHistorySource := Spies.SourceCounter(callsForBriefHistory, [(0UL, Line "I am a message.")])
    let actual = Dweller.Explain context Dummies.ValidSessionIdentifier Dummies.ValidDwellerIdentifier
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 7)
    Assert.AreEqual(1UL, callsForGetDweller.Value)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.AreEqual(1UL, callsForBriefHistory.Value)
    Assert.AreEqual(1UL, callsForGetStatistic.Value)


[<Test>]
let ``Explain.It returns empty list when that dweller does not exist for the given session.`` () =
    let calledGetDwellerList = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, [])
    let actual = Dweller.Explain context Dummies.ValidSessionIdentifier Dummies.ValidDwellerIdentifier
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 0)
    Assert.IsTrue(calledGetDwellerList.Value)
