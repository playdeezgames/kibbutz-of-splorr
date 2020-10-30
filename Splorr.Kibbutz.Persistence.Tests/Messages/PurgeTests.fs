module Messages.PurgeTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business

[<Test>]
let ``Purge.It purges messages for the given session.``() =
    MessagesImplementation.Put (Dummies.ValidSessionIdentfier, [Text "text1" ])
    MessagesImplementation.Purge Dummies.ValidSessionIdentfier
    MessagesImplementation.Put (Dummies.ValidSessionIdentfier, [Text "text2" ])
    let actual = MessagesImplementation.Get Dummies.ValidSessionIdentfier
    Assert.AreEqual([Text "text2"], actual)
