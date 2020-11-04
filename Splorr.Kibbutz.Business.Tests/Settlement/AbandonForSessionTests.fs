module Settlement.AbandonForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``AbandonSettlementForSession.It does nothing when no settlement exists.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    let actual = Settlement.AbandonSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)

[<Test>]
let ``AbandonSettlementForSession.It abandons a settlement when a settlement exists.`` () =
    let calledGetSettlement = ref false
    let calledPutSettlement = ref false
    let calledGetDwellerList = ref false
    let callsForPutDweller = ref 0UL
    let callsForAssignToSession = ref 0UL
    let callsForPurgeLog = ref 0UL
    let context = Contexts.TestContext()
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, Some Dummies.ValidSettlement)
    (context :> SettlementRepository.PutSettlementForSessionContext).settlementSink := Spies.Expect(calledPutSettlement, (Dummies.ValidSessionIdentifier, None))
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.PutContext).dwellerSingleSink := Spies.SinkCounter(callsForPutDweller)
    (context :> DwellerRepository.AssignToSessionContext).dwellerSessionSink := Spies.SinkCounter(callsForAssignToSession)
    (context :> DwellerRepository.PurgeLogsForDwellerContext).dwellerLogPurger := Spies.SinkCounter(callsForPurgeLog)
    let actual = Settlement.AbandonSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(2, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledPutSettlement.Value)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.AreEqual(3UL, callsForPutDweller.Value)
    Assert.AreEqual(3UL, callsForAssignToSession.Value)
    Assert.AreEqual(3UL, callsForPurgeLog.Value)

