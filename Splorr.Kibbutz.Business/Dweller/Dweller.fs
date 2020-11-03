namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module Dweller =
    let internal ShortDescribeSexGenes
            (sexGenes : SexGenes option)
            : string =
        match sexGenes with
        | Some XX ->
            "F"
        | Some XY ->
            "M"
        | _ ->
            "-"
    

    let private LongDescribeSexGenes
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
                Line (sprintf "Sex: %s" (dweller.sexGenes |> LongDescribeSexGenes))
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

    let private AssignWhenDwellerDoesNotExistMessages = 
        [
            Line "There is no such dweller in this settlement."
        ]
    let private SuccessfulAssignmentOfDwellerMessages =
        [
            Line "You update the dweller's assignment."
        ]

    let CompleteAssignmentOfDweller
            (context : CommonContext)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            (assignment:Assignment)
            : unit =
        {dweller with 
            assignment = assignment}
        |> Some
        |> DwellerRepository.Put context identifier
    
    let Assign
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier: DwellerIdentifier)
            (assignment : Assignment)
            : Message list =
        match DwellerRepository.GetForSession context session identifier with
        | Some dweller ->
            CompleteAssignmentOfDweller context identifier dweller assignment
            SuccessfulAssignmentOfDwellerMessages
        | _ ->
            AssignWhenDwellerDoesNotExistMessages
            

    let FindIdentifierForName = DwellerRepository.FindIdentifierForName
