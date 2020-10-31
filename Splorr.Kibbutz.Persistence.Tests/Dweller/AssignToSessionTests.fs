module Dweller.AssignToSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``GetListForSession.It gets a list of dwellers for a session.`` () =
    Dweller.GetListForSessionTests.``GetListForSession.It gets a list of dwellers for a session.``()
    DwellerIdentifierStore.AssignToSession (Dummies.ValidDwellerIdentifier, Some Dummies.ValidSessionIdentfier)
    let actual = DwellerIdentifierStore.GetListForSession Dummies.ValidSessionIdentfier
    Assert.AreEqual([Dummies.ValidDwellerIdentifier], actual)
