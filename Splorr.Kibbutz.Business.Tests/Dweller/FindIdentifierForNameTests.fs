module Dweller.FindIdentifierForNameTests

open NUnit.Framework
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Kibbutz.Model

[<Test>]
let ``FindIdentifierForName.It finds an identifier for a given name that exists.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource :=
        Spies.Source(calledFindIdentifier, Some Dummies.ValidDwellerIdentifier)
    let actual = Dweller.FindIdentifierForName context Dummies.ValidSessionIdentifier "yermom"
    Assert.AreEqual(Some Dummies.ValidDwellerIdentifier, actual)
    Assert.IsTrue(calledFindIdentifier.Value)


[<Test>]
let ``FindIdentifierForName.It returns none for a given name that does not exist.`` () =
    let calledFindIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> DwellerRepository.FindIdentifierForNameContext).dwellerIdentifierForNameSource :=
        Spies.Source(calledFindIdentifier, None)
    let actual = Dweller.FindIdentifierForName context Dummies.ValidSessionIdentifier "nachomama"
    Assert.AreEqual(None, actual)
    Assert.IsTrue(calledFindIdentifier.Value)


