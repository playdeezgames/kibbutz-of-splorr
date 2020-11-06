namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal DwellerAdvancer = 
    let private AdvanceExistingDweller
            (context : CommonContext)
            (session : SessionIdentifier)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            : Message list =
        match dweller.assignment with
        | Rest ->
            DwellerRepository.LogForDweller context (identifier, turn, Line "Rested.")
            [
                dweller.name |> sprintf "Dweller %s rests." |> Line
            ]
        | Explore ->
            DwellerRepository.LogForDweller context (identifier, turn, Line "Explored.")
            [
                dweller.name |> sprintf "Dweller %s explores." |> Line
            ]

    let private AdvanceDweller
            (context : CommonContext)
            (session : SessionIdentifier)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            : Message list =
        DwellerRepository.Get context identifier
        |> Option.map (AdvanceExistingDweller context session turn identifier)
        |> Option.defaultValue []

    let internal AdvanceDwellers
            (context : CommonContext)
            (session : SessionIdentifier)
            (turn : TurnCounter)
            : Message list =
        DwellerRepository.GetListForSession context session
        |> List.map (AdvanceDweller context session turn)
        |> List.reduce (@)


