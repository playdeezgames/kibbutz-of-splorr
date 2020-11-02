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
            (identifier, Some {name = ""; sexGenes = None; location = (0,0); assignment = Assignment.Rest }))
    |> Map.ofList
let ValidSettlement : Settlement =
    {
        turnCounter = 0UL
        vowels = Map.empty
        consonants = Map.empty
        nameLengthGenerator = Map.empty
        nameStartGenerator = Map.empty
    }