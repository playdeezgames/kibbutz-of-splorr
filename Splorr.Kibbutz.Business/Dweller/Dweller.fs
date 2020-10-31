namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerRepository =
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

module internal Dweller =
    let private ExplainExistingDweller
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            : unit =
        Messages.Put
            context
            session
            [
                Line ""
                Line (sprintf "Dweller: %s" (identifier.ToString()))
                Line (sprintf "Sex Genes: %s" (dweller.sexGenes.ToString()))
            ]

    let internal Explain
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : unit =
        DwellerRepository.Get context identifier
        |> Option.iter
            (ExplainExistingDweller context session identifier)
