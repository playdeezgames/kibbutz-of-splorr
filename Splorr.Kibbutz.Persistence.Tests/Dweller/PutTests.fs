module Dweller.PutTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``Put.It puts a dweller.`` () =
    Dweller.GetTests.``Get.It gets dweller.``()

    DwellerStore.Put (Dummies.ValidDwellerIdentifier, Some Dummies.ValidDweller)
    let actual = DwellerStore.Get Dummies.ValidDwellerIdentifier
    Assert.AreEqual(Some Dummies.ValidDweller, actual)