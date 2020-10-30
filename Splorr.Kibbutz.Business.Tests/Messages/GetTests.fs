module Messages.GetTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common

[<Test>]
let ``Get.It retrieves messages for a session.`` () =
    let calledGetMessages = ref false
    let context = Contexts.TestContext()
    (context :> Messages.GetContext).sessionMessageSource := Spies.Source(calledGetMessages, [])
    let actual = Messages.Get context Dummies.ValidSessionIdentifier
    Assert.AreEqual([], actual)
    Assert.IsTrue(calledGetMessages.Value)

