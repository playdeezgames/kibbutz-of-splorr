module Dweller.GetTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``Get.It gets dweller.`` () =
    DwellerStore.Put (Dummies.ValidDwellerIdentifier, None)
    let actual = DwellerStore.Get Dummies.ValidDwellerIdentifier
    Assert.AreEqual(None, actual)