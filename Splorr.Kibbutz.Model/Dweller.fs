namespace Splorr.Kibbutz.Model

open System

type Dweller = 
    {
        name : string
        sexGenes : SexGenes option
        location : Location
        assignment : Assignment
    }
