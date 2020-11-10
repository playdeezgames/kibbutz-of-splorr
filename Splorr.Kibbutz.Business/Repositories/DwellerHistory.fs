namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerHistoryRepository =
    type DwellerLogSink = DwellerIdentifier * TurnCounter * Message -> unit
    type AddHistoryContext = 
        abstract member dwellerLogSink : DwellerLogSink ref
    let internal AddHistory
            (context : CommonContext) =
        (context :?> AddHistoryContext).dwellerLogSink.Value

    type DwellerLogPurger = DwellerIdentifier -> unit
    type PurgeHistoryContext =
        abstract member dwellerLogPurger : DwellerLogPurger ref
    let internal PurgeHistory
            (context : CommonContext) =
        (context :?> PurgeHistoryContext).dwellerLogPurger.Value

    type DwellerBriefHistorySource = DwellerIdentifier -> (TurnCounter * Message) list
    type GetBriefHistoryContext =
        abstract member dwellerBriefHistorySource : DwellerBriefHistorySource ref
    let internal GetBriefHistory
            (context : CommonContext) =
        (context :?> GetBriefHistoryContext).dwellerBriefHistorySource.Value

    type DwellerPageHistorySource = DwellerIdentifier * uint64 -> (TurnCounter * Message) list
    type GetPageContext =
        abstract member dwellerPageHistorySource : DwellerPageHistorySource ref
    let internal GetPage
            (context : CommonContext) =
        (context :?> GetPageContext).dwellerPageHistorySource.Value

    type DwellerPageCountHistorySource = DwellerIdentifier -> uint64
    type GetPageCountContext =
        abstract member dwellerPageCountHistorySource : DwellerPageCountHistorySource ref
    let internal GetPageCount
            (context : CommonContext) =
        (context :?> GetPageCountContext).dwellerPageCountHistorySource.Value

