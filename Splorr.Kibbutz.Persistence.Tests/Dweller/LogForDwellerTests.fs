module Dweller.LogForDwellerTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``LogForDweller.It adds a message to the given dweller's log.`` () =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    DwellerLogStore.LogForDweller
        (Dummies.ValidDwellerIdentifier, 0UL, Line "I am a message.")
    DwellerLogStore.LogForDweller
        (Dummies.ValidDwellerIdentifier, 1UL, Line "I am another message.")
    let actual = DwellerLogStore.GetBriefHistory Dummies.ValidDwellerIdentifier
    Assert.AreEqual(
        [
            1UL, Line "I am another message."
            0UL, Line "I am a message." 
        ], actual)
        

