module Dweller.GetListForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``GetListForSession.It gets a list of dwellers for a session.`` () =
    DwellerIdentifierStore.GetListForSession Dummies.ValidSessionIdentfier
    |> List.iter
        (fun identifier -> DwellerIdentifierStore.AssignToSession (identifier, None))
    let actual = DwellerIdentifierStore.GetListForSession Dummies.ValidSessionIdentfier
    Assert.AreEqual([], actual)
