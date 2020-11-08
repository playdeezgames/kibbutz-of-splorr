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

let private WithCommonAssignmentContext 
        (dwellerTable : Map<DwellerIdentifier, Dweller option>)
        (test:Contexts.TestContext -> unit) =
    let calledGetDwellerList = ref false
    let callsForGetDweller = ref 0UL
    let calledPutSettlement = ref false
    let calledGetSettlement = ref false
    let callsForLogForDweller = ref 0UL
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := 
        Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource :=
        Spies.SourceTable(callsForGetDweller, dwellerTable)
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := 
        Spies.Source(calledGetSettlement, Some Dummies.ValidSettlement)
    (context :> SettlementRepository.PutSettlementForSessionContext).settlementSink := 
        Spies.Expect(calledPutSettlement, (Dummies.ValidSessionIdentifier, Some { Dummies.ValidSettlement with turnCounter=Dummies.ValidSettlement.turnCounter + 1UL }))
    (context :> DwellerLogRepository.LogForDwellerContext).dwellerLogSink :=
        Spies.SinkCounter(callsForLogForDweller)
    test context    
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledPutSettlement.Value)
    Assert.AreEqual(3UL, callsForLogForDweller.Value)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.AreEqual(3UL, callsForGetDweller.Value)

[<Test>]
let ``Advance.It advances an existing settlement by one turn when all dwellers are resting.`` () =
    WithCommonAssignmentContext Dummies.ValidDwellerTable
        (fun context ->
            let actual = Settlement.Advance context Dummies.ValidSessionIdentifier
            Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 4))

[<Test>]
let ``Advance.It advances an existing settlement by one turn and moves dwellers around when all dwellers are exploring.`` () =
    let callsForPutDweller = ref 0UL
    let dwellerTable = Dummies.AssignAllDwellers Explore
    WithCommonAssignmentContext dwellerTable
        (fun context ->
            (context :> DwellerRepository.PutContext).dwellerSingleSink := Spies.SinkCounter(callsForPutDweller)
            (context :> RandomUtility.RandomContext).random := Random(0)
            let actual = Settlement.Advance context Dummies.ValidSessionIdentifier
            Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 4))
    Assert.AreEqual(3UL, callsForPutDweller.Value)
