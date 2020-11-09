module DwellerInventory.GetPageTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Model

let private SetDummyInventory
        (howMany : int)
        (identifier:DwellerIdentifier) 
        : unit =
    DwellerInventoryStore.PurgeItems identifier
    [1..howMany]
    |> List.iter (fun _ -> DwellerInventoryStore.AddItem (identifier, Berry))

[<Test>]
let ``GetPage.It retrives the first page of items from a dweller's inventory when zero is passed.`` () =
    SetDummyInventory 12 Dummies.ValidDwellerIdentifier
    let actual = DwellerInventoryStore.GetPage (Dummies.ValidDwellerIdentifier, 0UL)
    Assert.AreEqual(10, actual.Length)

[<Test>]
let ``GetPage.It retrives the first page of items from a dweller's inventory when 1 is passed.`` () =
    SetDummyInventory 8 Dummies.ValidDwellerIdentifier
    let actual = DwellerInventoryStore.GetPage (Dummies.ValidDwellerIdentifier, 1UL)
    Assert.AreEqual(8, actual.Length)

[<Test>]
let ``GetPage.It retrives the second page of items from a dweller's inventory when 2 is passed.`` () =
    SetDummyInventory 16 Dummies.ValidDwellerIdentifier
    let actual = DwellerInventoryStore.GetPage (Dummies.ValidDwellerIdentifier, 2UL)
    Assert.AreEqual(6, actual.Length)

[<Test>]
let ``GetPageCount.It returns zero when there are no records.`` () =
    SetDummyInventory 0 Dummies.ValidDwellerIdentifier
    let actual = DwellerInventoryStore.GetPageCount Dummies.ValidDwellerIdentifier
    Assert.AreEqual(0UL, actual)

[<Test>]
let ``GetPageCount.It returns 1 when there is one record.`` () =
    SetDummyInventory 1 Dummies.ValidDwellerIdentifier
    let actual = DwellerInventoryStore.GetPageCount Dummies.ValidDwellerIdentifier
    Assert.AreEqual(1UL, actual)

[<Test>]
let ``GetPageCount.It returns 1 when there is a full page of record.`` () =
    SetDummyInventory 10 Dummies.ValidDwellerIdentifier
    let actual = DwellerInventoryStore.GetPageCount Dummies.ValidDwellerIdentifier
    Assert.AreEqual(1UL, actual)


[<Test>]
let ``GetPageCount.It returns 2 when there is a full page of record plus one.`` () =
    SetDummyInventory 11 Dummies.ValidDwellerIdentifier
    let actual = DwellerInventoryStore.GetPageCount Dummies.ValidDwellerIdentifier
    Assert.AreEqual(2UL, actual)
