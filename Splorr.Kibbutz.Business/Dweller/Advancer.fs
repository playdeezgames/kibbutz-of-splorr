namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

type private Direction =
    | North
    | East
    | South
    | West
    with
        static member ToDelta (direction : Direction) : Location =
            match direction with
            | North -> (0,-1)
            | East -> (1,0)
            | South -> (0,1)
            | West -> (-1,0)
        static member ToString (direction : Direction) : string =
            match direction with
            | North -> "north"
            | East -> "east"
            | South -> "south"
            | West -> "west"
            

module internal DwellerAdvancer = 
    let private Rest
            (context : CommonContext)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            : unit =
        DwellerLogRepository.LogForDweller context (identifier, turn, Line "Rested.")

    let private Explore
            (context : CommonContext)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            : unit =
        let direction= 
            [ North; East; South; West ]
            |> RandomUtility.PickFromListRandomly context
        let newLocation =
            direction
            |> Direction.ToDelta
            |> Location.Add dweller.location
        {dweller with
            location = newLocation}
        |> Some
        |> DwellerRepository.Put context identifier
        DwellerLogRepository.LogForDweller context 
            (identifier, 
                turn, 
                    Line 
                        (sprintf "Moved %s to %s." 
                            (direction |> Direction.ToString) 
                            (newLocation |> Location.ToString)))

    let private Gather
            (context : CommonContext)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            : unit =
        DwellerInventoryRepository.AddItem context (identifier, Berry)
        DwellerLogRepository.LogForDweller context (identifier, turn, Line "Gathered.")

    let private AdvanceExistingDweller
            (context : CommonContext)
            (turn : TurnCounter)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            : Message list =
        match dweller.assignment with
        | Explore ->
            Explore context turn identifier dweller
            [
                dweller.name |> sprintf "Dweller %s explores." |> Line
            ]
        | Gather ->
            Gather context turn identifier dweller
            [
                dweller.name |> sprintf "Dweller %s gathers." |> Line
            ]
        | Rest ->
            Rest context turn identifier
            [
                dweller.name |> sprintf "Dweller %s rests." |> Line
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


