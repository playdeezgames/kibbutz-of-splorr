namespace Splorr.Kibbutz.Business

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
                Line (sprintf "Dweller: %s" dweller.name)
                Line (dweller.location |> Location.ToString |> sprintf "Location: %s")
                Line (dweller.assignment |> Assignment.ToString |> sprintf "Assignment: %s")
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
