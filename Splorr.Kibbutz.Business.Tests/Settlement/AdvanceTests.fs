module Settlement.AdvanceTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Advance.It returns a message when the session has no settlement.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    let actual = Settlement.Advance context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)

[<Test>]
let ``Advance.It advances an existing settlement by one turn.`` () =
    let calledGetSettlement = ref false
    let calledPutSettlement = ref false
    let calledGetDwellerList = ref false
    let callsForGetDweller = ref 0UL
    let callsForLogForDweller = ref 0UL
    let context = Contexts.TestContext()
    (context :> DwellerRepository.LogForDwellerContext).dwellerLogSink :=
        Spies.SinkCounter(callsForLogForDweller)
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := 
        Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource :=
        Spies.SourceTable(callsForGetDweller, Dummies.ValidDwellerTable)
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := 
        Spies.Source(calledGetSettlement, Some Dummies.ValidSettlement)
    (context :> SettlementRepository.PutSettlementForSessionContext).settlementSink := 
        Spies.Expect(calledPutSettlement, (Dummies.ValidSessionIdentifier, Some { Dummies.ValidSettlement with turnCounter=Dummies.ValidSettlement.turnCounter + 1UL }))
    let actual = Settlement.Advance context Dummies.ValidSessionIdentifier
    Assert.AreEqual(4, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledPutSettlement.Value)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.AreEqual(3UL, callsForGetDweller.Value)
    Assert.AreEqual(3UL, callsForLogForDweller.Value)
