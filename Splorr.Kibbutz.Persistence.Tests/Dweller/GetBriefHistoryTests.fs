module Dweller.GetBriefHistoryTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``GetBriefHistory.It has no messages when the dweller's log has been purged.`` () =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    let actual = 
        DwellerLogStore.GetBriefHistory
            Dummies.ValidDwellerIdentifier
    Assert.AreEqual([], actual)


[<Test>]
let ``GetBriefHistory.It has messages when the dweller's log has added to.`` () =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    DwellerLogStore.LogForDweller (Dummies.ValidDwellerIdentifier, 0UL, Line "I am a message.")
    let actual = 
        DwellerLogStore.GetBriefHistory
            Dummies.ValidDwellerIdentifier
    Assert.AreEqual(1UL, actual.Length)
