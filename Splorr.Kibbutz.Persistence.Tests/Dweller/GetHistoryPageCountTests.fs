module DwellerLog.GetHistoryPageCountTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

let private makeLogEntries 
        (howMany :int)
        : unit =
    [1..howMany]
    |> List.iter (fun _ -> DwellerLogStore.LogForDweller (Dummies.ValidDwellerIdentifier, 0UL, Line "Message"))

[<Test>]
let ``GetHistoryPageCount.It returns 2 when there is a one more than a full page of data.``() =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    makeLogEntries 11
    let actual = DwellerLogStore.GetHistoryPageCount Dummies.ValidDwellerIdentifier
    Assert.AreEqual(2UL, actual)

[<Test>]
let ``GetHistoryPageCount.It returns 1 when there is a full page of data.``() =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    makeLogEntries 10
    let actual = DwellerLogStore.GetHistoryPageCount Dummies.ValidDwellerIdentifier
    Assert.AreEqual(1UL, actual)

[<Test>]
let ``GetHistoryPageCount.It returns 1 when there is a partial page of data.``() =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    makeLogEntries 5
    let actual = DwellerLogStore.GetHistoryPageCount Dummies.ValidDwellerIdentifier
    Assert.AreEqual(1UL, actual)

[<Test>]
let ``GetHistoryPageCount.It gets the count of pages for the given dweller.``() =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    makeLogEntries 33
    let actual = DwellerLogStore.GetHistoryPageCount Dummies.ValidDwellerIdentifier
    Assert.AreEqual(4UL, actual)

[<Test>]
let ``GetHistoryPageCount.It returns zero when there are no pages for the given dweller.``() =
    DwellerLogStore.PurgeLogsForDweller Dummies.ValidDwellerIdentifier
    let actual = DwellerLogStore.GetHistoryPageCount Dummies.ValidDwellerIdentifier
    Assert.AreEqual(0UL, actual)


