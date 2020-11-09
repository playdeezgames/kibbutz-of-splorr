module DwellerInventory.AddItemTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Model

[<Test>]
let ``AddItem.It adds an item to a dweller's inventory.`` () =
    DwellerInventoryStore.PurgeItems(Dummies.ValidDwellerIdentifier)
    DwellerInventoryStore.AddItem (Dummies.ValidDwellerIdentifier, Berry)
    let actual = DwellerInventoryStore.GetPage (Dummies.ValidDwellerIdentifier, 1UL)
    Assert.AreEqual([Berry], actual)
