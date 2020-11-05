module ExplainDwellerCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Parse.It returns an ExplainDweller command when "dweller yermom" is entered when yermom exists as a dweller.`` () =
    let calledFindDweller = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource := 
        Spies.Source(calledFindDweller, Some Dummies.ValidDwellerIdentifier)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "dweller"; "yermom" ]
    Assert.AreEqual(Some (ExplainDweller Dummies.ValidDwellerIdentifier), actual)
    Assert.IsTrue(calledFindDweller.Value)

[<Test>]
let ``Parse.It returns an Invalid command when "dweller" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "dweller" ]
    Assert.AreEqual(Some (Invalid "dweller"), actual)

[<Test>]
let ``Parse.It returns an Invalid command when "dweller nachomama" is entered when nachomama does not exist as a dweller.`` () =
    let calledFindDweller = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource := 
        Spies.Source(calledFindDweller, None)
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "dweller"; "nachomama" ]
    Assert.AreEqual(Some (Invalid "dweller nachomama"), actual)
    Assert.IsTrue(calledFindDweller.Value)


