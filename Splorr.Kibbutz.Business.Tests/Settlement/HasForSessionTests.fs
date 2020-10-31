module Settlement.HasSettlementForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``HasSettlementForSession.It returns false when there is no settlement for the session.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    let actual = Settlement.HasSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.IsFalse(actual)
    Assert.IsTrue(calledGetSettlement.Value)

[<Test>]
let ``HasSettlementForSession.It returns true when there is a settlement for the session.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, Some { turnCounter=0UL })
    let actual = Settlement.HasSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.IsTrue(actual)
    Assert.IsTrue(calledGetSettlement.Value)

