namespace Splorr.Kibbutz.Business

open Splorr.Common
open Splorr.Kibbutz.Model
open System

module Messages =
    type SessionMessageSource = SessionIdentifier -> Message list
    type GetContext =
        abstract member sessionMessageSource : SessionMessageSource ref
    let Get
            (context : CommonContext) =
        (context :?> GetContext).sessionMessageSource.Value
    
    type SessionMessagesSink = SessionIdentifier * Message list -> unit 
    type PutContext =
        abstract member sessionMessagesSink : SessionMessagesSink ref
    let Put
            (context : CommonContext) 
            (session : SessionIdentifier)
            (messages : Message list)
            : unit =
        (context :?> PutContext).sessionMessagesSink.Value (session, messages)

    type SessionMessagesPurge = SessionIdentifier -> unit
    type PurgeContext = 
        abstract member sessionMessagesPurge : SessionMessagesPurge ref
    let Purge
            (context : CommonContext) =
        (context :?> PurgeContext).sessionMessagesPurge.Value


