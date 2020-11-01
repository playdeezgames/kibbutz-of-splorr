module Dweller.GenerateIdentifierTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``GenerateIdentifier.It generates a non-colliding identifier.`` () =
    let identifier = DwellerIdentifierStore.GenerateIdentifier()
    let actual = DwellerStore.Get identifier
    Assert.AreEqual(None, actual)
    
