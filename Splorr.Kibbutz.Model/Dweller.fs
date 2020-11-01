﻿namespace Splorr.Kibbutz.Model

open System

type SexGenes =
    | XX
    | XY

type Dweller = 
    {
        name : string
        sexGenes : SexGenes option
    }

type DwellerIdentifier = Guid