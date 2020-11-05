module HelpCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation

[<Test>]
let ``Parse.It returns an Help command when "help" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "help" ]
    Assert.AreEqual(Some Help, actual)

[<Test>]
let ``Parse.It returns an Invalid command when "help blah" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "help"; "blah" ]
    Assert.AreEqual(Some (Invalid "help blah"), actual)
