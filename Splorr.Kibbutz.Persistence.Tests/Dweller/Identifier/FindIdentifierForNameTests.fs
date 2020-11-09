module Dweller.FindIdentifierForNameTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``FindIdentifierForName.It returns the identifier associated with the given dweller name for the given session.`` () =
    DwellerStore.Put (Dummies.ValidDwellerIdentifier, Some Dummies.ValidDweller)
    DwellerIdentifierStore.AssignToSession(Dummies.ValidDwellerIdentifier, Some Dummies.ValidSessionIdentfier)
    let actual = DwellerStore.FindIdentifierForName (Dummies.ValidSessionIdentfier, Dummies.ValidDweller.name)
    Assert.AreEqual(Some Dummies.ValidDwellerIdentifier, actual)


