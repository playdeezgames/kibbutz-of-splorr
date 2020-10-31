module Messages.PutTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``Put.It puts a message for the given session.``() =
    MessagesStore.Purge Dummies.ValidSessionIdentfier
    MessagesStore.Put (Dummies.ValidSessionIdentfier, [Text "text1" ])
    MessagesStore.Put (Dummies.ValidSessionIdentfier, [Text "text2" ])
    let actual = MessagesStore.Get Dummies.ValidSessionIdentfier
    Assert.AreEqual([Text "text1"; Text "text2"], actual)