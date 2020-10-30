module Messages.PutTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business

[<Test>]
let ``Put.It puts a message for the given session.``() =
    MessagesImplementation.Purge Dummies.ValidSessionIdentfier
    MessagesImplementation.Put (Dummies.ValidSessionIdentfier, [Text "text1" ])
    MessagesImplementation.Put (Dummies.ValidSessionIdentfier, [Text "text2" ])
    let actual = MessagesImplementation.Get Dummies.ValidSessionIdentfier
    Assert.AreEqual([Text "text1"; Text "text2"], actual)