module HistoryCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Parse.It returns an Invalid command when "history" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "history" ]
    Assert.AreEqual(Some (Invalid "history"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "history nachomama" is entered and nachomama does not exist.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, None)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "history"; "nachomama" ]
    Assert.AreEqual(Some (Invalid "history nachomama"), actual)
    Assert.IsTrue(calledFindIdentifier.Value)

[<Test>]
let ``Parse.It returns a History command when "history yermom" is entered and yermom exists.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, Some Dummies.ValidDwellerIdentifier)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "history"; "yermom" ]
    Assert.AreEqual(Some (History (Dummies.ValidDwellerIdentifier, 1UL)), actual)
    Assert.IsTrue(calledFindIdentifier.Value)

[<Test>]
let ``Parse.It returns an Invalid command when "history yermom page" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "history"; "yermom"; "page" ]
    Assert.AreEqual(Some (Invalid "history yermom page"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "history nachomama page 2" is entered and 'nachomama' does not exist.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, None)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "history"; "nachomama"; "page"; "2" ]
    Assert.AreEqual(Some (Invalid "history nachomama page 2"), actual)
    Assert.IsTrue(calledFindIdentifier.Value)


[<Test>]
let ``Parse.It returns an Invalid command when "history nachomama 2" is entered and 'nachomama' does not exist.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, None)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "history"; "nachomama"; "2" ]
    Assert.AreEqual(Some (Invalid "history nachomama 2"), actual)
    Assert.IsTrue(calledFindIdentifier.Value)


[<Test>]
let ``Parse.It returns a History command when "history yermom page 2" is entered and 'yermom' exists.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, Some Dummies.ValidDwellerIdentifier)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "history"; "yermom"; "page"; "2" ]
    Assert.AreEqual(Some (History (Dummies.ValidDwellerIdentifier, 2UL)), actual)
    Assert.IsTrue(calledFindIdentifier.Value)

[<Test>]
let ``Parse.It returns a History command when "history yermom 2" is entered and 'yermom' exists.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource
        := Spies.Source(calledFindIdentifier, Some Dummies.ValidDwellerIdentifier)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "history"; "yermom"; "2" ]
    Assert.AreEqual(Some (History (Dummies.ValidDwellerIdentifier, 2UL)), actual)
    Assert.IsTrue(calledFindIdentifier.Value)
