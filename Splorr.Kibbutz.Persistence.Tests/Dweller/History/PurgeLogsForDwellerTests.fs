module Dweller.PurgeLogsForDwellerTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``PurgeLogsForDweller.It removes all logs for a given dweller.`` () =
    DwellerLogStore.LogForDweller
        (Dummies.ValidDwellerIdentifier, 0UL, Line "I am a message.")
    DwellerLogStore.PurgeLogsForDweller
        Dummies.ValidDwellerIdentifier
    let actual = DwellerLogStore.GetBriefHistory Dummies.ValidDwellerIdentifier
    Assert.AreEqual([], actual)
