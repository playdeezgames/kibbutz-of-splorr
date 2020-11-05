module AbandonSettlementCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation

[<Test>]
let ``Parse.It returns an AbandonSettlement command when "abandon" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "abandon" ]
    Assert.AreEqual(Some AbandonSettlement, actual)


[<Test>]
let ``Parse.It returns an Invalid command when "abandon blah" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "abandon"; "blah" ]
    Assert.AreEqual(Some (Invalid "abandon blah"), actual)
