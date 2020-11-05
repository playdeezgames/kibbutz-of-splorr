module ListDwellersCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation

[<Test>]
let ``Parse.It returns a ListDwellers command when "dwellers" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "dwellers" ]
    Assert.AreEqual(Some ListDwellers, actual)

[<Test>]
let ``Parse.It returns a Invalid command when "dwellers blah" is entered.`` () =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "dwellers"; "blah" ]
    Assert.AreEqual(Some (Invalid "dwellers blah"), actual)


