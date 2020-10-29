module GameNewTests

open NUnit.Framework
open Splorr.Kibbutz.Presentation
open Splorr.Tests.Common
open System

[<Test>]
let ``New.It generates a new session.`` () =
    let calledPurgeMessages = ref false
    let calledPutMessages = ref false
    let context = Contexts.TestContext()
    (context :> Messages.PurgeContext).sessionMessagesPurge := Spies.Sink(calledPurgeMessages)
    (context :> Messages.PutContext).sessionMessagesSink := Spies.Sink(calledPutMessages)
    let actual = Game.New context
    Assert.AreNotEqual(Guid.Empty, actual)
    Assert.IsTrue(calledPurgeMessages.Value)
    Assert.IsTrue(calledPutMessages.Value)
    

