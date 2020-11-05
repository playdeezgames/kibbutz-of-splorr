module QuitCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation

[<Test>]
let ``Parse.It returns a Quit command when "quit" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "quit" ]
    Assert.AreEqual(Some Quit, actual)

[<Test>]
let ``Parse.It returns a Invalid command when "quit blah" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "quit"; "blah" ]
    Assert.AreEqual(Some (Invalid "quit blah"), actual)
