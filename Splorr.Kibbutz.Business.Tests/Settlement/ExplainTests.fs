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
    let calledGetDwellerList = ref false
    let callsForGetDweller = ref 0UL
    let context = Contexts.TestContext()
    (context :> Messages.PutContext).sessionMessagesSink := Spies.Sink(calledPutMessages)
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, Some { turnCounter = 0UL })
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource := Spies.SourceTable(callsForGetDweller, Dummies.ValidDwellerTable)
    Settlement.Explain context Dummies.ValidSessionIdentifier
    Assert.IsTrue(calledPutMessages.Value)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.AreEqual(3UL, callsForGetDweller.Value)


