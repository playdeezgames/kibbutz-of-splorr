module Messages.GetTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business

[<Test>]
let ``Get.It retrieves messages for the given session.``() =
    MessagesStore.Purge Dummies.ValidSessionIdentfier
    MessagesStore.Put (Dummies.ValidSessionIdentfier, [Text "text" ])
    let actual = MessagesStore.Get Dummies.ValidSessionIdentfier
    Assert.AreEqual([Text "text"], actual)

