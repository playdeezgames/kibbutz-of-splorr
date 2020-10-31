module Settlement.AdvanceTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Advance.It returns a message when the session has no settlement.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> Settlement.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    let actual = Settlement.Advance context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)

[<Test>]
let ``Advance.It advances an existing settlement by one turn.`` () =
    let calledGetSettlement = ref false
    let calledPutSettlement = ref false
    let context = Contexts.TestContext()
    (context :> Settlement.GetSettlementForSessionContext).settlementSource := 
        Spies.Source(calledGetSettlement, Some { turnCounter = 0UL })
    (context :> Settlement.PutSettlementForSessionContext).settlementSink := 
        Spies.Expect(calledPutSettlement, (Dummies.ValidSessionIdentifier, Some { turnCounter = 1UL }))
    let actual = Settlement.Advance context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledPutSettlement.Value)
