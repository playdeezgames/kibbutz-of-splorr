module Settlement.ExplainTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Explain.It explains that there is no settlement when there is no settlement.`` () =
    let calledPutMessages = ref false
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> Messages.PutContext).sessionMessagesSink := Spies.Sink(calledPutMessages)
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    Settlement.Explain context Dummies.ValidSessionIdentifier
    Assert.IsTrue(calledPutMessages.Value)
    Assert.IsTrue(calledGetSettlement.Value)

[<Test>]
let ``Explain.It explains the state of the settlement when the settlement exists.`` () =
    let calledPutMessages = ref false
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> Messages.PutContext).sessionMessagesSink := Spies.Sink(calledPutMessages)
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, Some { turnCounter = 0UL })
    Settlement.Explain context Dummies.ValidSessionIdentifier
    Assert.IsTrue(calledPutMessages.Value)
    Assert.IsTrue(calledGetSettlement.Value)


