namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System


module SessionNamesStore =
    let private sessionNames : Map<SessionIdentifier, Set<string>> ref = ref Map.empty

    let ClearNames
            (session : SessionIdentifier)
            : unit =
        sessionNames.Value 
        |> Map.tryFind session
        |> Option.iter
            (fun _ ->
                (sessionNames := sessionNames.Value |> Map.remove session))

    let AddName
            (session : SessionIdentifier,
             name : string)
            : unit =
        let names = 
            sessionNames.Value 
            |> Map.tryFind session 
            |> Option.defaultValue Set.empty
            |> Set.add name
        sessionNames :=
            sessionNames.Value
            |> Map.add session names

    let HasName
            (session : SessionIdentifier,
             name: string)
            : bool =
        sessionNames.Value
        |> Map.tryFind session
        |> Option.map (Set.contains name)
        |> Option.defaultValue false
