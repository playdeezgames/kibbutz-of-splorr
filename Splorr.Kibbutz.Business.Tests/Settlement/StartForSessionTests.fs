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
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 1)
    Assert.IsTrue(calledGetSettlement.Value)

[<Test>]
let ``StartSettlementForSession.It creates a new settlement when a settlement does not exist.`` () =
    let callsForPutContext = ref 0UL
    let calledGetSettlement = ref false
    let callsForPutDweller = ref 0UL
    let calledAssignDwellerSession = ref false
    let callsForGenerateIdentifier = ref 0UL
    let calledClearNames = ref false
    let callsForCheckName = ref 0UL
    let callsForAddName = ref 0UL
    let callsForLogForDweller = ref 0UL
    let callsForPutDwellerStatistic = ref 0UL
    let context = Contexts.TestContext()
    (context :> DwellerStatisticRepository.PutContext).dwellerStatisticSink :=
        Spies.SinkCounter(callsForPutDwellerStatistic)
    (context :> DwellerRepository.GenerateIdentifierContext).dwellerIdentifierSource := 
        Spies.SourceHook(callsForGenerateIdentifier, 
            fun () -> Guid.NewGuid())
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    (context :> SettlementRepository.PutSettlementForSessionContext).settlementSink := 
        Spies.Validate(callsForPutContext, 
            fun (identifier, settlement) ->
                Assert.AreEqual(Dummies.ValidSessionIdentifier ,identifier)
                Assert.AreNotEqual(None, settlement)
                true)
    (context :> DwellerRepository.PutContext).dwellerSingleSink := 
        Spies.Validate(callsForPutDweller,
            fun (identifier, dweller) ->
                Assert.AreNotEqual(Guid.Empty ,identifier)
                Assert.IsTrue(dweller.IsSome)
                Assert.AreNotEqual("", dweller.Value.name)
                true)
    (context :> DwellerRepository.AssignToSessionContext).dwellerSessionSink := Spies.Sink(calledAssignDwellerSession)
    (context :> RandomUtility.RandomContext).random := (Random(0))
    (context :> SessionRepository.ClearNamesContext).sessionNamePurger := Spies.Sink(calledClearNames)
    (context :> SessionRepository.CheckNameContext).sessionNameValidator := Spies.SourceCounter(callsForCheckName, false)
    (context :> SessionRepository.AddNameContext).sessionNameSink := Spies.SinkCounter(callsForAddName)
    (context :> DwellerHistoryRepository.AddHistoryContext).dwellerLogSink := Spies.SinkCounter(callsForLogForDweller)
    let actual =
        Settlement.StartSettlementForSession context Dummies.ValidSessionIdentifier
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 1)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.AreEqual(1UL, callsForPutContext.Value)
    Assert.AreEqual(3UL, callsForPutDweller.Value)
    Assert.IsTrue(calledAssignDwellerSession.Value)
    Assert.AreEqual(3UL, callsForGenerateIdentifier.Value)
    Assert.IsTrue(calledClearNames.Value)
    Assert.AreEqual(3UL, callsForCheckName.Value)
    Assert.AreEqual(3UL, callsForAddName.Value)
    Assert.AreEqual(3UL, callsForLogForDweller.Value)
    Assert.AreEqual(6UL, callsForPutDwellerStatistic.Value)


