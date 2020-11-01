﻿namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal Dweller =
    let private DescribeName
            (identifier : DwellerIdentifier)
            : string =
        identifier.ToString().Replace('0','o').Replace('1','i').Replace('2','z').Replace('3','m').Replace('4','h').Replace('5','s').Replace('6','k').Replace('7','l').Replace('8','j').Replace('9','g')

    let private DescribeSexGenes
            (sexGenes : SexGenes option)
            : string =
        match sexGenes with
        | Some XX ->
            "Biologically Female"
        | Some XY ->
            "Biologically Male"
        | _ ->
            "Other"


    let private ExplainExistingDweller
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            : unit =
        Messages.Put
            context
            session
            [
                Line ""
                Line (sprintf "Dweller: %s" (identifier |> DescribeName))
                Line (sprintf "Sex: %s" (dweller.sexGenes |> DescribeSexGenes))
            ]

    let internal Explain
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : unit =
        DwellerRepository.Get context identifier
        |> Option.iter
            (ExplainExistingDweller context session identifier)

    let private sexGenesTable =
        [
            (Some XX), 49.5
            (Some XY), 49.5
            (None), 1.0
        ]
        |> Map.ofList

    let internal Create
            (context : CommonContext)
            : Dweller =
        let sexGenes = 
            RandomUtility.GenerateFromWeightedValues context sexGenesTable
        {
            name = ""
            sexGenes = sexGenes
        }
