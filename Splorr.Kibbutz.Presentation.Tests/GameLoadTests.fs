module GameLoadTests

open NUnit.Framework
open Splorr.Kibbutz.Presentation

[<Test>]
let ``Load.It loads nothing`` () =
    let context = Contexts.TestContext()
    let actual = Game.Load context
    Assert.AreEqual(None, actual)