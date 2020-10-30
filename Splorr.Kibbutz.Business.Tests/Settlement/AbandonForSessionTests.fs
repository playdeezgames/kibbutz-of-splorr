module Settlement.AbandonForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``AbandonSettlementForSession.It does nothing when no settlement exists.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> Settlement.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    let actual = Settlement.AbandonSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)

[<Test>]
let ``AbandonSettlementForSession.It abandons a settlement when a settlement exists.`` () =
    let calledGetSettlement = ref false
    let calledPutSettlement = ref false
    let context = Contexts.TestContext()
    (context :> Settlement.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, Some { turnCounter=0UL})
    (context :> Settlement.PutSettlementForSessionContext).settlementSink := Spies.Expect(calledPutSettlement, (Dummies.ValidSessionIdentifier, None))
    let actual = Settlement.AbandonSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledPutSettlement.Value)

