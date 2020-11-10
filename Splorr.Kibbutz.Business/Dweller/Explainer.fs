namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerExplainer =
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

    let internal RenderHistoryAsMessage
            (turn: TurnCounter, message:Message)
            : Message =
        Group 
            [
                Text (sprintf "Turn %u: " turn)
                message
            ]

    let internal RenderInventoryAsMessage
            (item : Item)
            : Message =
        item.ToString() |> Line

    let private ExplainExistingDweller
            (context : CommonContext)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            : Message list =
        let dwellerLogMessages =
            DwellerHistoryRepository.GetBriefHistory context identifier
            |> List.map RenderHistoryAsMessage
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
            Line "Brief History:"
            Group dwellerLogMessages
        ]

    let private ExplainExistingDwellerForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : Message =
        DwellerRepository.Get context identifier
        |> Option.map (ExplainExistingDweller context identifier)
        |> Option.defaultValue []
        |> Group

    let internal Explain
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            : Message =
        if DwellerSession.ExistsForSession context session identifier then
            ExplainExistingDwellerForSession context session identifier
        else
            [] |> Group
