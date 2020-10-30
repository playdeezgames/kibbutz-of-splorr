module Messages.PurgeTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Purge.It purges the messages for a session.`` () =
    let calledPurgeMessages = ref false
    let context = Contexts.TestContext()
    (context :> Messages.PurgeContext).sessionMessagesPurge := Spies.Sink(calledPurgeMessages)
    Messages.Purge context Dummies.ValidSessionIdentifier
    Assert.IsTrue(calledPurgeMessages.Value)

