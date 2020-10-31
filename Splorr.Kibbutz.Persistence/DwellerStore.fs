namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System

module DwellerStore =
    let private dwellerIdentifiers : Map<SessionIdentifier, DwellerIdentifier list> ref = ref Map.empty
    let private dwellers : Map<DwellerIdentifier, Dweller> ref = ref Map.empty

    let GetListForSession
            (session : SessionIdentifier)
            : DwellerIdentifier list =
        dwellerIdentifiers.Value
        |> Map.tryFind session
        |> Option.defaultValue []

    let Get
            (identifier : DwellerIdentifier)
            : Dweller option =
        dwellers.Value
        |> Map.tryFind identifier
