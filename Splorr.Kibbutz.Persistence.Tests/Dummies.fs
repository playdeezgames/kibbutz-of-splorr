module Dummies

open System
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

let ValidSessionIdentfier : SessionIdentifier = Guid.NewGuid()
let ValidDwellerIdentifier : DwellerIdentifier = Guid.NewGuid()
let ValidSettlement : Settlement =
    {
        turnCounter = 0UL
        vowels = []
        consonants = []
        nameLengthGenerator = Map.empty
        nameStartGenerator = Map.empty
    }