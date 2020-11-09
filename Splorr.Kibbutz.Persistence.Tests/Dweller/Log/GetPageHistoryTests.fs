module Dweller.GetPageHistoryTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``GetPageHistory.It has no messages when the dweller's log has been purged.`` () =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    let actual = 
        DwellerLogStore.GetPageHistory
            (Dummies.ValidDwellerIdentifier, 1UL)
    Assert.AreEqual([], actual)

let private addDwellerMessages() =
    [0UL..14UL]
    |> List.iter 
        (fun index -> 
            DwellerLogStore.LogForDweller (Dummies.ValidDwellerIdentifier, index, Line (sprintf "Message #%u" index)))

[<Test>]
let ``GetPageHistory.It has messages on the first page when the dweller's log has added to.`` () =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    addDwellerMessages()
    let actual = 
        DwellerLogStore.GetPageHistory
            (Dummies.ValidDwellerIdentifier, 1UL)
    Assert.AreEqual(10UL, actual.Length)


[<Test>]
let ``GetPageHistory.It has messages on the second page when the dweller's log has added to.`` () =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    addDwellerMessages()
    let actual = 
        DwellerLogStore.GetPageHistory
            (Dummies.ValidDwellerIdentifier, 2UL)
    Assert.AreEqual(5UL, actual.Length)

[<Test>]
let ``GetPageHistory.It has no messages on the third page when the dweller's log has added to.`` () =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    addDwellerMessages()
    let actual = 
        DwellerLogStore.GetPageHistory
            (Dummies.ValidDwellerIdentifier, 3UL)
    Assert.AreEqual(0UL, actual.Length)
