namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

type private Direction =
    | North
    | East
    | South
    | West

module internal DwellerAdvancer = 
    let private Rest
            (context : CommonContext)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            : unit =
        DwellerRepository.LogForDweller context (identifier, turn, Line "Rested.")

    let private Explore
            (context : CommonContext)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            : unit =
        //pick a random direction
        //move in that direction
        DwellerRepository.LogForDweller context (identifier, turn, Line "Explored.")

    let private AdvanceExistingDweller
            (context : CommonContext)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            : Message list =
        match dweller.assignment with
        | Rest ->
            Rest context turn identifier
            [
                dweller.name |> sprintf "Dweller %s rests." |> Line
            ]
        | Explore ->
            Explore context turn identifier dweller
            [
                dweller.name |> sprintf "Dweller %s explores." |> Line
            ]

    let private AdvanceDweller
            (context : CommonContext)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            : Message list =
        DwellerRepository.Get context identifier
        |> Option.map (AdvanceExistingDweller context turn identifier)
        |> Option.defaultValue []

    let internal AdvanceDwellers
            (context : CommonContext)
            (session : SessionIdentifier)
            (turn : TurnCounter)
            : Message list =
        DwellerRepository.GetListForSession context session
        |> List.map (AdvanceDweller context turn)
        |> List.reduce (@)


