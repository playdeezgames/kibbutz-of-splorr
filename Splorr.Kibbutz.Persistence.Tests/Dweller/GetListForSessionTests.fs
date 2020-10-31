module Dweller.GetListForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``GetListForSession.It gets a list of dwellers for a session.`` () =
    let actual = DwellerStore.GetListForSession Dummies.ValidSessionIdentfier
    Assert.AreEqual([], actual)
