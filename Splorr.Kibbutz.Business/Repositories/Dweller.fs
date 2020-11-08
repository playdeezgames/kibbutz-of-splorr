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

module DwellerRepository =
    type DwellerIdentifierSource = unit -> DwellerIdentifier
    type GenerateIdentifierContext =
        abstract member dwellerIdentifierSource : DwellerIdentifierSource ref
    let internal GenerateIdentifier
            (context : CommonContext) =
        (context :?> GenerateIdentifierContext).dwellerIdentifierSource.Value()

    type SessionDwellerSource = SessionIdentifier -> DwellerIdentifier list
    type GetListForSessionContext =
        abstract member sessionDwellerSource : SessionDwellerSource ref
    let internal GetListForSession
            (context : CommonContext) =
        (context :?> GetListForSessionContext).sessionDwellerSource.Value

    type DwellerSingleSource = DwellerIdentifier -> Dweller option
    type GetContext =
        abstract member dwellerSingleSource : DwellerSingleSource ref
    let internal Get
            (context : CommonContext) =
        (context :?> GetContext).dwellerSingleSource.Value

    type DwellerSingleSink = DwellerIdentifier * Dweller option -> unit
    type PutContext =
        abstract member dwellerSingleSink : DwellerSingleSink ref
    let internal Put
            (context : CommonContext)
            (identifier : DwellerIdentifier)
            (dweller : Dweller option)
            : unit =
        (context :?> PutContext).dwellerSingleSink.Value (identifier, dweller)

    type DwellerSessionSink = DwellerIdentifier * SessionIdentifier option -> unit
    type AssignToSessionContext =
        abstract member dwellerSessionSink : DwellerSessionSink ref
    let internal AssignToSession 
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : unit =
        (context :?> AssignToSessionContext).dwellerSessionSink.Value (identifier, Some session)
    let internal RemoveFromSession
            (context : CommonContext)
            (identifier : DwellerIdentifier)
            : unit =
        (context :?> AssignToSessionContext).dwellerSessionSink.Value (identifier, None)

    type DwellerIdentifierForNameSource = SessionIdentifier * string -> DwellerIdentifier option
    type FindIdentifierForNameContext =
        abstract member dwellerIdentifierForNameSource : DwellerIdentifierForNameSource ref
    let internal FindIdentifierForName
        (context : CommonContext)
        (session : SessionIdentifier)
        (dwellerName : string)
        : DwellerIdentifier option =
            (context :?> FindIdentifierForNameContext).dwellerIdentifierForNameSource.Value (session, dwellerName)



