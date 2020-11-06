namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal SettlementAdvancer =
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

    let private AdvanceDwellers
            (context : CommonContext)
            (session : SessionIdentifier)
            (turn : TurnCounter)
            : Message list =
        DwellerRepository.GetListForSession context session
        |> List.map (AdvanceDweller context session turn)
        |> List.reduce (@)

    let private UpdateSettlementTurnCounter
            (context : CommonContext)
            (session : SessionIdentifier)
            (settlement : Settlement)
            : unit =
        {settlement with 
            turnCounter = settlement.turnCounter + 1UL}
        |> Some
        |> SettlementRepository.PutSettlementForSession 
            context 
            session

    let private AdvanceExistingSettlement
            (context : CommonContext)
            (session : SessionIdentifier)
            (settlement : Settlement)
            : Message list =
        let messages = 
            AdvanceDwellers context session settlement.turnCounter
        UpdateSettlementTurnCounter context session settlement
        List.append 
            messages
            [Hued (Green, Line "You advance your settlement to the next turn.")]

    let internal Advance
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        match SettlementRepository.GetSettlementForSession context session with
        | Some settlement ->
            AdvanceExistingSettlement context session settlement
        | _ ->
            [Hued (Red, Line "You have no settlement to advance.")]
