module GameNewTests

open NUnit.Framework
open Splorr.Kibbutz.Presentation
open System

[<Test>]
let ``New.It generates a new session.`` () =
    let context = Contexts.TestContext()
    let actual = Game.New context
    Assert.AreNotEqual(Guid.Empty, actual)
    

