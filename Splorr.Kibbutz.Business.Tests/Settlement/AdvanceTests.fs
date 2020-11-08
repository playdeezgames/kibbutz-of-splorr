module Settlement.AdvanceTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Kibbutz.Model
open Splorr.Common
open System

[<Test>]
let ``Advance.It returns a message when the session has no settlement.`` () =
    let calledGetSettlement = ref false
    let context = Contexts.TestContext()
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlement, None)
    let actual = Settlement.Advance context Dummies.ValidSessionIdentifier
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 1)
    Assert.IsTrue(calledGetSettlement.Value)

[<Test>]
let ``Advance.It advances an existing settlement by one turn when all dwellers are resting.`` () =
    let calledGetSettlement = ref false
    let calledPutSettlement = ref false
    let calledGetDwellerList = ref false
    let callsForGetDweller = ref 0UL
    let callsForLogForDweller = ref 0UL
    let context = Contexts.TestContext()
    (context :> DwellerLogRepository.LogForDwellerContext).dwellerLogSink :=
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
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 4)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledPutSettlement.Value)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.AreEqual(3UL, callsForGetDweller.Value)
    Assert.AreEqual(3UL, callsForLogForDweller.Value)


[<Test>]
let ``Advance.It advances an existing settlement by one turn and moves dwellers around when all dwellers are exploring.`` () =
    let calledGetSettlement = ref false
    let calledPutSettlement = ref false
    let calledGetDwellerList = ref false
    let callsForGetDweller = ref 0UL
    let callsForLogForDweller = ref 0UL
    let callsForPutDweller = ref 0UL

    let dwellerTable = 
        Dummies.ValidDwellerTable
        |> Map.map
            (fun _ d ->
                d 
                |> Option.map(fun dweller -> {dweller with assignment = Explore}))

    let context = Contexts.TestContext()
    (context :> DwellerRepository.PutContext).dwellerSingleSink := Spies.SinkCounter(callsForPutDweller)
    (context :> RandomUtility.RandomContext).random := Random(0)
    (context :> DwellerLogRepository.LogForDwellerContext).dwellerLogSink :=
        Spies.SinkCounter(callsForLogForDweller)
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := 
        Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource :=
        Spies.SourceTable(callsForGetDweller, dwellerTable)
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := 
        Spies.Source(calledGetSettlement, Some Dummies.ValidSettlement)
    (context :> SettlementRepository.PutSettlementForSessionContext).settlementSink := 
        Spies.Expect(calledPutSettlement, (Dummies.ValidSessionIdentifier, Some { Dummies.ValidSettlement with turnCounter=Dummies.ValidSettlement.turnCounter + 1UL }))
    let actual = Settlement.Advance context Dummies.ValidSessionIdentifier
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 4)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledPutSettlement.Value)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.AreEqual(3UL, callsForGetDweller.Value)
    Assert.AreEqual(3UL, callsForLogForDweller.Value)
    Assert.AreEqual(3UL, callsForPutDweller.Value)
