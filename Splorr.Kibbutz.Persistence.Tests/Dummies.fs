module Dummies

open System
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

let ValidSessionIdentfier : SessionIdentifier = Guid.NewGuid()
let ValidDwellerIdentifier : DwellerIdentifier = Guid.NewGuid()
let ValidSettlement : Settlement =
    {
        turnCounter = 0UL
        vowels = Map.empty
        consonants = Map.empty
        nameLengthGenerator = Map.empty
        nameStartGenerator = Map.empty
    }
let ValidDweller : Dweller =
    { name = "yermom"; sexGenes = Some XX; location = (2,3); assignment = Assignment.Rest}