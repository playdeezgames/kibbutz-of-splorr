module InvalidCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation

[<Test>]
let ``Parse.It returns a Invalid command when "blah" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "blah" ]
    Assert.AreEqual(Some (Invalid "blah"), actual)


