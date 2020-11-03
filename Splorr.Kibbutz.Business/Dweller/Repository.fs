namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

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

    let internal ExistsForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier: DwellerIdentifier)
            : bool =
        GetListForSession context session
        |> List.exists ((=) identifier)

    let internal GetForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : Dweller option =
        if ExistsForSession context session identifier then
            Get context identifier
        else
            None

    type DwellerIdentifierForNameSource = SessionIdentifier * string -> DwellerIdentifier option
    type FindIdentifierForNameContext =
        abstract member dwellerIdentifierForNameSource : DwellerIdentifierForNameSource ref
    let internal FindIdentifierForName
        (context : CommonContext)
        (session : SessionIdentifier)
        (dwellerName : string)
        : DwellerIdentifier option =
            (context :?> FindIdentifierForNameContext).dwellerIdentifierForNameSource.Value (session, dwellerName)

    let internal GetCountForSession
            (context : CommonContext) =
        GetListForSession context
        >> List.length
        >> uint64

