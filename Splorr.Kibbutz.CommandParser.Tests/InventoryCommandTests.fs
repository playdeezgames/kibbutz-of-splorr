module InventoryCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Parse.It returns an Invalid command when "inventory" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "inventory" ]
    Assert.AreEqual(Some (Invalid "inventory"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "inventory nachomama" is entered and nachomama does not exist.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, None)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "inventory"; "nachomama" ]
    Assert.AreEqual(Some (Invalid "inventory nachomama"), actual)
    Assert.IsTrue(calledFindIdentifier.Value)

[<Test>]
let ``Parse.It returns a Inventory command when "inventory yermom" is entered and yermom exists.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, Some Dummies.ValidDwellerIdentifier)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "inventory"; "yermom" ]
    Assert.AreEqual(Some (Inventory (Dummies.ValidDwellerIdentifier, 1UL)), actual)
    Assert.IsTrue(calledFindIdentifier.Value)

[<Test>]
let ``Parse.It returns an Invalid command when "inventory yermom page" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "inventory"; "yermom"; "page" ]
    Assert.AreEqual(Some (Invalid "inventory yermom page"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "inventory nachomama page 2" is entered and 'nachomama' does not exist.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, None)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "inventory"; "nachomama"; "page"; "2" ]
    Assert.AreEqual(Some (Invalid "inventory nachomama page 2"), actual)
    Assert.IsTrue(calledFindIdentifier.Value)


[<Test>]
let ``Parse.It returns an Invalid command when "inventory nachomama 2" is entered and 'nachomama' does not exist.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, None)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "inventory"; "nachomama"; "2" ]
    Assert.AreEqual(Some (Invalid "inventory nachomama 2"), actual)
    Assert.IsTrue(calledFindIdentifier.Value)


[<Test>]
let ``Parse.It returns a Inventory command when "inventory yermom page 2" is entered and 'yermom' exists.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, Some Dummies.ValidDwellerIdentifier)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "inventory"; "yermom"; "page"; "2" ]
    Assert.AreEqual(Some (Inventory (Dummies.ValidDwellerIdentifier, 2UL)), actual)
    Assert.IsTrue(calledFindIdentifier.Value)

[<Test>]
let ``Parse.It returns a Inventory command when "inventory yermom 2" is entered and 'yermom' exists.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, Some Dummies.ValidDwellerIdentifier)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "inventory"; "yermom"; "2" ]
    Assert.AreEqual(Some (Inventory (Dummies.ValidDwellerIdentifier, 2UL)), actual)
    Assert.IsTrue(calledFindIdentifier.Value)
