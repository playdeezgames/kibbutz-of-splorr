module Dweller.HistoryTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Kibbutz.Model

[<Test>]
let ``History.It returns an empty result set for a given dweller that does not exist.`` () =
    let calledGetListForSession = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource :=
        Spies.Source(calledGetListForSession, [])
    let actual =
        Dweller.History context Dummies.ValidSessionIdentifier Dummies.InvalidDwellerIdentifier 0UL
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 1)
    Assert.IsTrue(calledGetListForSession.Value)

[<Test>]
let ``History.It retrieves a page full of history for a given dweller.`` () =
    let calledGetListForSession = ref false
    let calledGetPageHistory = ref false
    let calledPageHistoryCount = ref false
    let context = Contexts.TestContext()
    (context :> DwellerHistoryRepository.GetPageCountContext).dwellerPageCountHistorySource :=
        Spies.Source(calledPageHistoryCount, Dummies.ValidDwellerHistory.Length |> uint64)
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource :=
        Spies.Source(calledGetListForSession, [ Dummies.ValidDwellerIdentifier ])
    (context :> DwellerHistoryRepository.GetPageContext).dwellerPageHistorySource :=
        Spies.Source(calledGetPageHistory, Dummies.ValidDwellerHistory)
    let actual =
        Dweller.History context Dummies.ValidSessionIdentifier Dummies.ValidDwellerIdentifier 0UL
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 2)
    Assert.IsTrue(calledGetListForSession.Value)
    Assert.IsTrue(calledGetPageHistory.Value)
    Assert.IsTrue(calledPageHistoryCount.Value)


