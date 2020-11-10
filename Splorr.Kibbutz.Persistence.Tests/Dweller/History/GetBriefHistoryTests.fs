module Dweller.GetBriefHistoryTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``GetBriefHistory.It has no messages when the dweller's log has been purged.`` () =
    DwellerHistoryStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    let actual = 
        DwellerHistoryStore.GetBriefHistory
            Dummies.ValidDwellerIdentifier
    Assert.AreEqual([], actual)


[<Test>]
let ``GetBriefHistory.It has messages when the dweller's log has added to.`` () =
    DwellerHistoryStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    DwellerHistoryStore.LogForDweller (Dummies.ValidDwellerIdentifier, 0UL, Line "I am a message.")
    let actual = 
        DwellerHistoryStore.GetBriefHistory
            Dummies.ValidDwellerIdentifier
    Assert.AreEqual(1UL, actual.Length)
