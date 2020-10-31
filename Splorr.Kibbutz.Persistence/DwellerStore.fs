namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System


module DwellerIdentifierStore =
    let private dwellerIdentifiers : Map<DwellerIdentifier, SessionIdentifier> ref = ref Map.empty

    let GetListForSession
            (session : SessionIdentifier)
            : DwellerIdentifier list =
        dwellerIdentifiers.Value
        |> Map.filter
            (fun _ s -> s = session)
        |> Map.toList
        |> List.map fst

    let AssignToSession
            (identifier : DwellerIdentifier, session : SessionIdentifier option)
            : unit =
        match session with
        | Some s ->
            dwellerIdentifiers :=
                dwellerIdentifiers.Value
                |> Map.add identifier s
        | None ->
            dwellerIdentifiers :=
                dwellerIdentifiers.Value
                |> Map.remove identifier

module DwellerStore =
    let private dwellers : Map<DwellerIdentifier, Dweller> ref = ref Map.empty

    let Get
            (identifier : DwellerIdentifier)
            : Dweller option =
        dwellers.Value
        |> Map.tryFind identifier

    let Put
            (identifier : DwellerIdentifier, dweller : Dweller option)
            : unit =
        match dweller with
        | Some d ->
            dwellers :=
                dwellers.Value
                |> Map.add identifier d
        | None ->
            dwellers :=
                dwellers.Value
                |> Map.remove identifier
