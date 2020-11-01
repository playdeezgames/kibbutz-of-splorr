namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module SettlementRepository =
    type SettlementSource = SessionIdentifier -> Settlement option
    type GetSettlementForSessionContext =
        abstract member settlementSource : SettlementSource ref
    let internal GetSettlementForSession
            (context : CommonContext) =
        (context :?> GetSettlementForSessionContext).settlementSource.Value

    type SettlementSink = SessionIdentifier * Settlement option -> unit
    type PutSettlementForSessionContext =
        abstract member settlementSink : SettlementSink ref
    let internal PutSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            (settlement : Settlement option)
            : unit =
        (context :?> PutSettlementForSessionContext).settlementSink.Value (session, settlement)
