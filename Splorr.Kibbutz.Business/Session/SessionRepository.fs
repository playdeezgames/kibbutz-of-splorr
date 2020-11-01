namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module SessionRepository =
    type SessionIdentifierSource = unit -> SessionIdentifier
    type GenerateIdentifierContext =
        abstract member sessionIdentifierSource : SessionIdentifierSource ref
    let internal GenerateIdentifier
            (context : CommonContext) =
        (context :?> GenerateIdentifierContext).sessionIdentifierSource.Value()

module Session =
    let GenerateIdentifier = SessionRepository.GenerateIdentifier