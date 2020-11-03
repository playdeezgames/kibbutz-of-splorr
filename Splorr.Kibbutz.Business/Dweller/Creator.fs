﻿namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerCreator = 
    let private sexGenesTable =
        [
            (Some XX), 49.5
            (Some XY), 49.5
            (None), 1.0
        ]
        |> Map.ofList

    let internal Create
            (context : CommonContext)
            (name : string)
            : Dweller =
        let sexGenes = 
            RandomUtility.GenerateFromWeightedValues context sexGenesTable
        {
            name = name
            sexGenes = sexGenes
            location = Location.Default
            assignment = Assignment.Default
        }