module StartSettlementCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation

[<Test>]
let ``Parse.It returns an StartSettlement command when "start" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "start" ]
    Assert.AreEqual(Some StartSettlement, actual)


[<Test>]
let ``Parse.It returns an Invalid command when "start blah" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "start"; "blah" ]
    Assert.AreEqual(Some (Invalid "start blah"), actual)

