module Messages.PurgeTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business

[<Test>]
let ``Purge.It purges messages for the given session.``() =
    MessagesStore.Put (Dummies.ValidSessionIdentfier, [Text "text1" ])
    MessagesStore.Purge Dummies.ValidSessionIdentfier
    MessagesStore.Put (Dummies.ValidSessionIdentfier, [Text "text2" ])
    let actual = MessagesStore.Get Dummies.ValidSessionIdentfier
    Assert.AreEqual([Text "text2"], actual)
