namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System


module DwellerIdentifierStore =
    let private dwellerIdentifiers : Map<DwellerIdentifier, SessionIdentifier> ref = ref Map.empty

    let GenerateIdentifier() : DwellerIdentifier =
        Guid.NewGuid()

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

