namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerSession =
    let internal ExistsForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier: DwellerIdentifier)
            : bool =
        DwellerRepository.GetListForSession context session
        |> List.exists ((=) identifier)

    let internal GetForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : Dweller option =
        if ExistsForSession context session identifier then
            DwellerRepository.Get context identifier
        else
            None

    let internal GetCountForSession
            (context : CommonContext) =
        DwellerRepository.GetListForSession context
        >> List.length
        >> uint64

    let internal GetDwellersForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Dweller list =
        DwellerRepository.GetListForSession context session
        |> List.map
            (fun identifier ->                     
                (DwellerRepository.Get context identifier |> Option.get))


