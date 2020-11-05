module AssignCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Kibbutz.Model

[<Test>]
let ``Parse.It returns an Invalid command when "assign" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "assign" ]
    Assert.AreEqual(Some (Invalid "assign"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "assign nachomama" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "assign"; "nachomama" ]
    Assert.AreEqual(Some (Invalid "assign nachomama"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "assign yermom" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "assign"; "yermom" ]
    Assert.AreEqual(Some (Invalid "assign yermom"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "assign yermom to" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "assign"; "yermom"; "to" ]
    Assert.AreEqual(Some (Invalid "assign yermom to"), actual)

[<Test>]
let ``Parse.It returns an Assign command when "assign yermom to rest" is entered when yermom exists as a dweller.`` () =
    let calledFindDweller = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource :=
        Spies.Source(calledFindDweller, Some Dummies.ValidDwellerIdentifier)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "assign"; "yermom"; "to"; "rest" ]
    Assert.AreEqual(Some (Assign (Dummies.ValidDwellerIdentifier, Rest)), actual)
    Assert.IsTrue(calledFindDweller.Value)

[<Test>]
let ``Parse.It returns an Assign command when "assign yermom to explore" is entered when yermom exists as a dweller.`` () =
    let calledFindDweller = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource :=
        Spies.Source(calledFindDweller, Some Dummies.ValidDwellerIdentifier)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "assign"; "yermom"; "to"; "explore" ]
    Assert.AreEqual(Some (Assign (Dummies.ValidDwellerIdentifier, Explore)), actual)
    Assert.IsTrue(calledFindDweller.Value)

[<Test>]
let ``Parse.It returns an Invalid command when "assign yermom to poop" is entered when yermom exists as a dweller.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "assign"; "yermom"; "to"; "poop" ]
    Assert.AreEqual(Some (Invalid "assign yermom to poop"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "assign yermom to rest now" is entered when yermom exists as a dweller.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "assign"; "yermom"; "to"; "rest"; "now" ]
    Assert.AreEqual(Some (Invalid "assign yermom to rest now"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "assign nachomama to rest" is entered when nachomama does not exist as a dweller.`` () =
    let calledFindDweller = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource :=
        Spies.Source(calledFindDweller, None)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "assign"; "nachomama"; "to"; "rest" ]
    Assert.AreEqual(Some (Invalid "assign nachomama to rest"), actual)
    Assert.IsTrue(calledFindDweller.Value)
