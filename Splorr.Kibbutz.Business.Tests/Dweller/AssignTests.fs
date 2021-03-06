module Dweller.AssignTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Kibbutz.Model

[<Test>]
let ``Assign.It does nothing when the dweller does not exist in the session.``() =
    let calledGetDwellerList = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, [])
    let actual = 
        Dweller.Assign 
            context
            Dummies.ValidSessionIdentifier
            Dummies.ValidDwellerIdentifier
            Explore
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 1)
    Assert.IsTrue(calledGetDwellerList.Value)


[<Test>]
let ``Assign.It does nothing when the dweller does not exist.``() =
    let calledGetDwellerList = ref false
    let calledGetDweller = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource := Spies.Source(calledGetDweller, None)
    let actual = 
        Dweller.Assign 
            context
            Dummies.ValidSessionIdentifier
            Dummies.ValidDwellerIdentifier
            Explore
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 1)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.IsTrue(calledGetDweller.Value)

[<Test>]
let ``Assign.It sets the assignment when the dweller exists in the session.``() =
    let calledGetDwellerList = ref false
    let calledGetDweller = ref false
    let calledPutDweller = ref false
    let calledGetSettlement = ref false
    let calledLogForDweller = ref false
    let context = Contexts.TestContext()
    (context :> DwellerHistoryRepository.AddHistoryContext).dwellerLogSink := Spies.Sink(calledLogForDweller)
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource :=
        Spies.Source(calledGetSettlement, Some Dummies.ValidSettlement)
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource := 
        Spies.Source(calledGetDwellerList, Dummies.ValidDwellerIdentifiers)
    (context :> DwellerRepository.GetContext).dwellerSingleSource := 
        Spies.Source(calledGetDweller, Dummies.ValidDweller)
    let mutatedDweller =
        {Dummies.ValidDweller.Value with assignment=Explore}
        |> Some
    (context :> DwellerRepository.PutContext).dwellerSingleSink := 
        Spies.Expect(calledPutDweller, (Dummies.ValidDwellerIdentifier, mutatedDweller))
    let actual = 
        Dweller.Assign 
            context
            Dummies.ValidSessionIdentifier
            Dummies.ValidDwellerIdentifier
            Explore
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 1)
    Assert.IsTrue(calledGetDwellerList.Value)
    Assert.IsTrue(calledGetDweller.Value)
    Assert.IsTrue(calledPutDweller.Value)
    Assert.IsTrue(calledGetSettlement.Value)
    Assert.IsTrue(calledLogForDweller.Value)
