﻿namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal Dweller =
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
                Line (sprintf "Dweller: %s" (identifier.ToString()))
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

    let internal Create
            (context : CommonContext)
            : Dweller =
        let random = Random()
        let sexGenes = 
            match random.Next(101) with
            | x when x < 50 -> Some XX
            | x when x < 100 -> Some XY
            | _ -> None
        {
            sexGenes = sexGenes
        }
