namespace Splorr.Kibbutz.Model

open System

type SexGenes =
    | XX
    | XY

type Dweller = 
    {
        sexGenes : SexGenes
    }

type DwellerIdentifier = Guid