namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal SettlementAdvancer =
    let internal Advance
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        match SettlementRepository.GetSettlementForSession context session with
        | Some settlement ->
            SettlementRepository.PutSettlementForSession context session (Some {settlement with turnCounter = settlement.turnCounter + 1UL})
            [Hued (Green, Line "You advance your settlement to the next turn.")]
        | _ ->
            [Hued (Red, Line "You have no settlement to advance.")]
