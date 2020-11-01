module Dummies

open Splorr.Kibbutz.Model
open Splorr.Kibbutz.Business
open System

let internal ValidSessionIdentifier : SessionIdentifier = Guid.NewGuid()
let internal ValidDwellerIdentifiers : DwellerIdentifier list = [ Guid.NewGuid(); Guid.NewGuid(); Guid.NewGuid() ]
let internal ValidDwellerTable : Map<DwellerIdentifier, Dweller option> =
    ValidDwellerIdentifiers
    |> List.map
        (fun identifier ->
            (identifier, Some {sexGenes = None}))
    |> Map.ofList