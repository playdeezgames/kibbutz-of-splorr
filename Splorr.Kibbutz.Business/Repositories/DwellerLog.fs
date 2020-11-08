namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerLogRepository =
    type DwellerLogSink = DwellerIdentifier * TurnCounter * Message -> unit
    type LogForDwellerContext = 
        abstract member dwellerLogSink : DwellerLogSink ref
    let internal LogForDweller
            (context : CommonContext) =
        (context :?> LogForDwellerContext).dwellerLogSink.Value

    type DwellerLogPurger = DwellerIdentifier -> unit
    type PurgeLogsForDwellerContext =
        abstract member dwellerLogPurger : DwellerLogPurger ref
    let internal PurgeLogsForDweller
            (context : CommonContext) =
        (context :?> PurgeLogsForDwellerContext).dwellerLogPurger.Value

    type DwellerBriefHistorySource = DwellerIdentifier -> (TurnCounter * Message) list
    type GetBriefHistoryContext =
        abstract member dwellerBriefHistorySource : DwellerBriefHistorySource ref
    let GetBriefHistory
            (context : CommonContext) =
        (context :?> GetBriefHistoryContext).dwellerBriefHistorySource.Value

    type DwellerPageHistorySource = DwellerIdentifier * uint64 -> (TurnCounter * Message) list
    type GetPageHistoryContext =
        abstract member dwellerPageHistorySource : DwellerPageHistorySource ref
    let GetPageHistory
            (context : CommonContext) =
        (context :?> GetPageHistoryContext).dwellerPageHistorySource.Value
