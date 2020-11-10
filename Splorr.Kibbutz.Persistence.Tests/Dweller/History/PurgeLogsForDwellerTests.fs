module Dweller.PurgeLogsForDwellerTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``PurgeLogsForDweller.It removes all logs for a given dweller.`` () =
    DwellerHistoryStore.LogForDweller
        (Dummies.ValidDwellerIdentifier, 0UL, Line "I am a message.")
    DwellerHistoryStore.PurgeLogsForDweller
        Dummies.ValidDwellerIdentifier
    let actual = DwellerHistoryStore.GetBriefHistory Dummies.ValidDwellerIdentifier
    Assert.AreEqual([], actual)
