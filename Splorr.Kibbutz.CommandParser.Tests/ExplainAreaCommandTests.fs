module ExplainAreaCommandTests

open Splorr.Kibbutz
open NUnit.Framework
open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Parse.It returns Invalid when 'area' is entered.``() =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "area" ]
    Assert.AreEqual(Some (Invalid "area"), actual)

[<Test>]
let ``Parse.It returns ExplainArea (0,0) when 'area 0 0' is entered.``() =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "area"; "0"; "0" ]
    Assert.AreEqual(Some (ExplainArea (0,0)), actual)

[<Test>]
let ``Parse.It returns ExplainArea (1,-1) when 'area 1 -1' is entered.``() =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "area"; "1"; "-1" ]
    Assert.AreEqual(Some (ExplainArea (1,-1)), actual)

[<Test>]
let ``Parse.It returns ExplainArea (-1,1) when 'area -1 1' is entered.``() =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "area"; "-1"; "1" ]
    Assert.AreEqual(Some (ExplainArea (-1,1)), actual)

[<Test>]
let ``Parse.It returns Invalid when 'area blah' is entered.``() =
    let context = Contexts.TestContext()
    let actual = CommandParser.Parse context Dummies.ValidSessionIdentifier [ "area"; "blah" ]
    Assert.AreEqual(Some (Invalid "area blah"), actual)
