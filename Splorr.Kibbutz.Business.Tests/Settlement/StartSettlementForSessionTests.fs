module Settlement.StartSettlementForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``StartSettlementForSession.It does nothing when a settlement already exists.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> Settlement.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, Some {iExistOnlyToHaveAFieldInTheRecord=0})
    let actual =
        Settlement.StartSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)


[<Test>]
let ``StartSettlementForSession.It creates a new settlement when a settlement does not exist.`` () =
    let calledPutContext = ref false
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> Settlement.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    (context :> Settlement.PutSettlementForSessionContext).settlementSink := Spies.Expect(calledPutContext, (Dummies.ValidSessionIdentifier, Some { iExistOnlyToHaveAFieldInTheRecord=0}))
    let actual =
        Settlement.StartSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledPutContext.Value)


