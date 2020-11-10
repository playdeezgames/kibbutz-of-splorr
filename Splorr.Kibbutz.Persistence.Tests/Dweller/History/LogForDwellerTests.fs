module Dweller.LogForDwellerTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``LogForDweller.It adds a message to the given dweller's log.`` () =
    DwellerHistoryStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    DwellerHistoryStore.LogForDweller
        (Dummies.ValidDwellerIdentifier, 0UL, Line "I am a message.")
    DwellerHistoryStore.LogForDweller
        (Dummies.ValidDwellerIdentifier, 1UL, Line "I am another message.")
    let actual = DwellerHistoryStore.GetBriefHistory Dummies.ValidDwellerIdentifier
    Assert.AreEqual(
        [
            1UL, Line "I am another message."
            0UL, Line "I am a message." 
        ], actual)
        

