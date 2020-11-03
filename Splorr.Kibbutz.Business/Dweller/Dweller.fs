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
            (dweller : Dweller)
            : Message list =
        [
            Group 
                [
                    Hued (Light Blue, Text "Dweller: ")
                    Hued (Magenta, Line (sprintf "%s" dweller.name))
                ]
            Group 
                [
                    Hued (Light Blue, Text "Location: ")
                    Hued (Magenta, Line (dweller.location |> Location.ToString |> sprintf "%s"))
                ]
            Group 
                [
                    Hued (Light Blue, Text "Assignment: ")
                    Hued (Magenta, Line (dweller.assignment |> Assignment.ToString |> sprintf "%s"))
                ]
            Group 
                [
                    Hued (Light Blue, Text "Sex: ")
                    Hued (Magenta, Line (sprintf "%s" (dweller.sexGenes |> LongDescribeSexGenes)))
                ]
        ]

    let private ExplainExistingDwellerForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : Message list =
        DwellerRepository.Get context identifier
        |> Option.map ExplainExistingDweller
        |> Option.defaultValue []

    let Explain
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : Message list =
        if DwellerRepository.ExistsForSession context session identifier then
            ExplainExistingDwellerForSession context session identifier
        else
            []

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
