namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

type SessionIdentifier = Guid
type Message = 
    | Text of string
    | Line of string


module Messages =
    type SessionMessageSource = SessionIdentifier -> Message list
    type GetContext =
        abstract member sessionMessageSource : SessionMessageSource ref
    let internal Get
            (context : CommonContext) =
        (context :?> GetContext).sessionMessageSource.Value
    
    type SessionMessagesSink = SessionIdentifier * Message list -> unit 
    type PutContext =
        abstract member sessionMessagesSink : SessionMessagesSink ref
    let internal Put
            (context : CommonContext) =
        (context :?> PutContext).sessionMessagesSink.Value

    type SessionMessagesPurge = SessionIdentifier -> unit
    type PurgeContext = 
        abstract member sessionMessagesPurge : SessionMessagesPurge ref
    let internal Purge
            (context : CommonContext) =
        (context :?> PurgeContext).sessionMessagesPurge.Value


