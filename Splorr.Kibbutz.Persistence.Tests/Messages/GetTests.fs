module Messages.GetTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business

[<Test>]
let ``Get.It retrieves messages for the given session.``() =
    MessagesImplementation.Purge Dummies.ValidSessionIdentfier
    MessagesImplementation.Put (Dummies.ValidSessionIdentfier, [Text "text" ])
    let actual = MessagesImplementation.Get Dummies.ValidSessionIdentfier
    Assert.AreEqual([Text "text"], actual)

