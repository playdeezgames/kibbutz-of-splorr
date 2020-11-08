namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal SettlementExplainer =
    let private ExplainSettlement
            (context : CommonContext)
            (session : SessionIdentifier)
            (settlement : Settlement)
            : unit =
        Messages.Put context session 
            [
                Group
                    [
                        Hued (Light Blue, Text "Turn: ")
                        Hued (Magenta, Line (sprintf "%u" settlement.turnCounter))
                    ]
            ]
        let dwellerCount =
            DwellerSession.GetCountForSession context session
        Messages.Put context session 
            [
                Group
                    [
                        Hued (Light Blue, Text "Dwellers: ")
                        Hued (Magenta, Line (sprintf "%u" dwellerCount))
                    ]
            ]

    let private ExplainNoSettlement
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Put context session [Hued (Blue, Line "You have no settlement.")]

    let internal Explain
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Put context session [Line ""]
        match SettlementRepository.GetSettlementForSession context session with
        | Some settlement ->
            ExplainSettlement context session settlement
        | _ ->
            ExplainNoSettlement context session
