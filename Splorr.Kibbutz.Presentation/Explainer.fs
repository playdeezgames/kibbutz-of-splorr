namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System

module internal Explainer =
    let private ExplainSettlement
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Put context session [Line "(the settlement will be explained here)"]

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
        if Settlement.HasSettlementForSession context session then
            ExplainSettlement context session
        else
            ExplainNoSettlement context session



