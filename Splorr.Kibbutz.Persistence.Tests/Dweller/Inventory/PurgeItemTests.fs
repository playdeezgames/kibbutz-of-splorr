module DwellerInventory.PurgeItemsTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Model

[<Test>]
let ``PurgeItems.It removes all items from a dweller's inventory.`` () =
    DwellerInventoryStore.AddItem (Dummies.ValidDwellerIdentifier, Berry)
    DwellerInventoryStore.PurgeItems Dummies.ValidDwellerIdentifier
    let actual = DwellerInventoryStore.GetPage (Dummies.ValidDwellerIdentifier, 1UL)
    Assert.AreEqual([], actual)
