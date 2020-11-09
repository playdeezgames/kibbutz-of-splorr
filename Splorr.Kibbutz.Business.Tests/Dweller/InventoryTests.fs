module Dweller.InventoryTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Kibbutz.Model

[<Test>]
let ``Inventory.It returns an empty result set for a given dweller that does not exist.`` () =
    let calledGetListForSession = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource :=
        Spies.Source(calledGetListForSession, [])
    let actual =
        Dweller.Inventory context Dummies.ValidSessionIdentifier Dummies.InvalidDwellerIdentifier 0UL
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 1)
    Assert.IsTrue(calledGetListForSession.Value)

[<Test>]
let ``Inventory.It retrieves a page full of history for a given dweller.`` () =
    let calledGetListForSession = ref false
    let calledGetPageInventory = ref false
    let calledPageInventoryCount = ref false
    let context = Contexts.TestContext()
    (context :> DwellerInventoryRepository.GetPageCountContext).dwellerInventoryPageCountSource :=
        Spies.Source(calledPageInventoryCount, Dummies.ValidDwellerInventory.Length |> uint64)
    (context :> DwellerRepository.GetListForSessionContext).sessionDwellerSource :=
        Spies.Source(calledGetListForSession, [ Dummies.ValidDwellerIdentifier ])
    (context :> DwellerInventoryRepository.GetPageContext).dwellerInventoryPageSource :=
        Spies.Source(calledGetPageInventory, Dummies.ValidDwellerInventory)
    let actual =
        Dweller.Inventory context Dummies.ValidSessionIdentifier Dummies.ValidDwellerIdentifier 0UL
    Assertions.ValidateMessageIsGroupWithGivenItemCount(actual, 2)
    Assert.IsTrue(calledGetListForSession.Value)
    Assert.IsTrue(calledGetPageInventory.Value)
    Assert.IsTrue(calledPageInventoryCount.Value)


