module Dummies

open Splorr.Kibbutz.Model
open Splorr.Kibbutz.Business
open System

let internal ValidSessionIdentifier : SessionIdentifier = Guid.NewGuid()
let internal ValidDwellerIdentifiers : DwellerIdentifier list = [ Guid.NewGuid(); Guid.NewGuid(); Guid.NewGuid() ]
let internal ValidDwellerIdentifier = ValidDwellerIdentifiers |> List.head
let internal InvalidDwellerIdentifier = Guid.Empty
let internal ValidDwellerTable : Map<DwellerIdentifier, Dweller option> =
    ValidDwellerIdentifiers
    |> List.map
        (fun identifier ->
            (identifier, Some {name = ""; sexGenes = None; location = (0,0); assignment = Assignment.Rest }))
    |> Map.ofList
let internal AssignAllDwellers (assignment : Assignment) : Map<DwellerIdentifier, Dweller option> =
    ValidDwellerTable
    |> Map.map
        (fun _ d ->
            d 
            |> Option.map(fun dweller -> {dweller with assignment = assignment}))
let internal ValidDweller = ValidDwellerTable.[ValidDwellerIdentifier]
let internal ValidSettlement : Settlement =
    {
        turnCounter = 0UL
        vowels = Map.empty
        consonants = Map.empty
        nameLengthGenerator = Map.empty
        nameStartGenerator = Map.empty
    }
let internal ValidDwellerHistory : (TurnCounter * Message) list =
    [
        1UL, Line "1"
        2UL, Line "2"
        3UL, Line "3"
        4UL, Line "4"
        5UL, Line "5"
    ]
let internal ValidDwellerInventory : Item list =
    [ Berry ]