module Messages.PutTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Put.It puts messages for a session.`` () =
    let calledPutMessages = ref false
    let context = Contexts.TestContext()
    (context :> Messages.PutContext).sessionMessagesSink := Spies.Sink(calledPutMessages)
    Messages.Put context Dummies.ValidSessionIdentifier []
    Assert.IsTrue(calledPutMessages.Value)

