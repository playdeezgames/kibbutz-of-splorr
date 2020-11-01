module Session.GenerateIdentifierTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System

[<Test>]
let ``GenerateIdentifier.It generates a non-colliding identifier.`` () =
    let actual = SessionIdentifierStore.GenerateIdentifier()
    Assert.AreNotEqual(Guid.Empty, actual)
    
