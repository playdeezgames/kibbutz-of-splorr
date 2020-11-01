namespace Splorr.Kibbutz.Model

open System

type SexGenes =
    | XX
    | XY

type Dweller = 
    {
        sexGenes : SexGenes option
    }

type DwellerIdentifier = string