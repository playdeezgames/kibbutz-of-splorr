module Settlement.StartSettlementForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open System

[<Test>]
let ``StartSettlementForSession.It does nothing when a settlement already exists.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, Some {turnCounter=0UL})
    let actual =
        Settlement.StartSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)


[<Test>]
let ``StartSettlementForSession.It creates a new settlement when a settlement does not exist.`` () =
    let calledPutContext = ref false
    let calledGetSettlement = ref false
    let calledPutDweller = ref false
    let calledAssignDwellerSession = ref false
    let callsForGenerateIdentifier = ref 0UL
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GenerateIdentifierContext).dwellerIdentifierSource := Spies.SourceHook(callsForGenerateIdentifier, fun () -> Guid.NewGuid())
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    (context :> SettlementRepository.PutSettlementForSessionContext).settlementSink := Spies.Expect(calledPutContext, (Dummies.ValidSessionIdentifier, Some { turnCounter=0UL}))
    (context :> DwellerRepository.PutContext).dwellerSingleSink := Spies.Sink(calledPutDweller)
    (context :> DwellerRepository.AssignToSessionContext).dwellerSessionSink := Spies.Sink(calledAssignDwellerSession)
    let actual =
        Settlement.StartSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledPutContext.Value)
    Assert.IsTrue(calledPutDweller.Value)
    Assert.IsTrue(calledAssignDwellerSession.Value)
    Assert.AreEqual(3UL, callsForGenerateIdentifier.Value)


