namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal SettlementAdvancer =
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
            DwellerAdvancer.AdvanceDwellers context session settlement.turnCounter
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
