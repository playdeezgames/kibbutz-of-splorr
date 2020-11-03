module Dummies

open Splorr.Kibbutz.Model
open Splorr.Kibbutz.Business
open System

let internal ValidSessionIdentifier : SessionIdentifier = Guid.NewGuid()
let internal ValidDwellerIdentifiers : DwellerIdentifier list = [ Guid.NewGuid(); Guid.NewGuid(); Guid.NewGuid() ]
let internal ValidDwellerIdentifier = ValidDwellerIdentifiers |> List.head
let internal ValidDwellerTable : Map<DwellerIdentifier, Dweller option> =
    ValidDwellerIdentifiers
    |> List.map
        (fun identifier ->
            (identifier, Some {name = ""; sexGenes = None; location = (0,0); assignment = Assignment.Rest }))
    |> Map.ofList
let internal ValidDweller = ValidDwellerTable.[ValidDwellerIdentifier]
let internal ValidSettlement : Settlement =
    {
        turnCounter = 0UL
        vowels = Map.empty
        consonants = Map.empty
        nameLengthGenerator = Map.empty
        nameStartGenerator = Map.empty
    }