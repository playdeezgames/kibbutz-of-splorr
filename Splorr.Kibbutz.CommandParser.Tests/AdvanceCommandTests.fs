module AdvanceCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation

[<Test>]
let ``Parse.It returns an Advance command when "advance" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "advance" ]
    Assert.AreEqual(Some Advance, actual)

[<Test>]
let ``Parse.It returns an Invalid command when "advance blah" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "advance"; "blah" ]
    Assert.AreEqual(Some (Invalid "advance blah"), actual)


