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
    
    type SessionNamePurger = SessionIdentifier -> unit
    type ClearNamesContext = 
        abstract member sessionNamePurger : SessionNamePurger ref
    let internal ClearNames
            (context : CommonContext) =
        (context :?> ClearNamesContext).sessionNamePurger.Value 


    type SessionNameValidator = SessionIdentifier * string -> bool
    type CheckNameContext =
        abstract member sessionNameValidator : SessionNameValidator ref
    let internal HasName
            (context : CommonContext)
            (session : SessionIdentifier)
            (name : string)
            : bool =
        (context :?> CheckNameContext).sessionNameValidator.Value (session, name)

    type SessionNameSink = SessionIdentifier * string -> unit
    type AddNameContext =
        abstract member sessionNameSink : SessionNameSink ref
    let internal AddName
            (context : CommonContext)
            (session: SessionIdentifier)
            (name : string)
            : unit =
        (context :?> AddNameContext).sessionNameSink.Value (session, name)

module Session =
    let GenerateIdentifier = SessionRepository.GenerateIdentifier