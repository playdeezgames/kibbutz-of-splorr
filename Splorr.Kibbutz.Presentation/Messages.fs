namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

type SessionIdentifier = Guid
type Hue =
    | Black
    | Blue
    | Green
    | Cyan
    | Red
    | Magenta
    | Yellow
    | Gray
    | Light of Hue
type Message = 
    | Text of string
    | Line of string
    | Hued of Hue * Message


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
            (context : CommonContext) 
            (session : SessionIdentifier)
            (messages : Message list)
            : unit =
        (context :?> PutContext).sessionMessagesSink.Value (session, messages)

    type SessionMessagesPurge = SessionIdentifier -> unit
    type PurgeContext = 
        abstract member sessionMessagesPurge : SessionMessagesPurge ref
    let internal Purge
            (context : CommonContext) =
        (context :?> PurgeContext).sessionMessagesPurge.Value


