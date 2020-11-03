namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System

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

    let private DoesDwellerMatchName
            (name : string) =
        Get
        >> Option.map
            (fun dweller -> dweller.name=name)
        >> Option.defaultValue false
    
    let FindIdentifierForName
            (session: SessionIdentifier, name : string) : DwellerIdentifier option =
        DwellerIdentifierStore.GetListForSession session
        |> List.filter (DoesDwellerMatchName name)
        |> List.tryExactlyOne

