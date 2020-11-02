namespace Splorr.Kibbutz.Model

open System

type SexGenes =
    | XX
    | XY

type Dweller = 
    {
        name : string
        sexGenes : SexGenes option
        location : Location
    }

type DwellerIdentifier = Guid