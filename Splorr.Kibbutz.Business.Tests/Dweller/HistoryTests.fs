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
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource :=
        Spies.Source(calledGetListForSession, [ Dummies.ValidDwellerIdentifier ])
    (context :> DwellerLogRepository.GetPageHistoryContext).dwellerPageHistorySource :=
        Spies.Source(calledGetPageHistory, Dummies.ValidDwellerHistory)
    let actual =
        Dweller.History context Dummies.ValidSessionIdentifier Dummies.ValidDwellerIdentifier 0UL
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 2)
    Assert.IsTrue(calledGetListForSession.Value)
    Assert.IsTrue(calledGetPageHistory.Value)


