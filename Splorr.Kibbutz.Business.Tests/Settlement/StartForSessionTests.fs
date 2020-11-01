module Settlement.StartSettlementForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open System
open Splorr.Common

[<Test>]
let ``StartSettlementForSession.It does nothing when a settlement already exists.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, Some Dummies.ValidSettlement)
    let actual =
        Settlement.StartSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)


[<Test>]
let ``StartSettlementForSession.It creates a new settlement when a settlement does not exist.`` () =
    let callsForPutContext = ref 0UL
    let calledGetSettlement = ref false
    let calledPutDweller = ref false
    let calledAssignDwellerSession = ref false
    let callsForGenerateIdentifier = ref 0UL
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GenerateIdentifierContext).dwellerIdentifierSource := Spies.SourceHook(callsForGenerateIdentifier, fun () -> Guid.NewGuid())
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    (context :> SettlementRepository.PutSettlementForSessionContext).settlementSink := 
        Spies.Validate(callsForPutContext, 
            fun (identifier, settlement) ->
                Assert.AreEqual(Dummies.ValidSessionIdentifier ,identifier)
                Assert.AreNotEqual(None, settlement)
                true)
    (context :> DwellerRepository.PutContext).dwellerSingleSink := Spies.Sink(calledPutDweller)
    (context :> DwellerRepository.AssignToSessionContext).dwellerSessionSink := Spies.Sink(calledAssignDwellerSession)
    (context :> RandomUtility.RandomContext).random := (Random(0))
    let actual =
        Settlement.StartSettlementForSession context Dummies.ValidSessionIdentifier
    Assert.AreEqual(1, actual.Length)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.AreEqual(1UL, callsForPutContext.Value)
    Assert.IsTrue(calledPutDweller.Value)
    Assert.IsTrue(calledAssignDwellerSession.Value)
    Assert.AreEqual(3UL, callsForGenerateIdentifier.Value)


