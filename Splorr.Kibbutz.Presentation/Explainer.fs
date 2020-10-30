namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

module internal Explainer =
    let private ExplainSettlement
            (context : CommonContext)
            (session : SessionIdentifier)
            (settlement : Settlement)
            : unit =
        Messages.Put context (session, [Line "(the settlement will be explained here)"])

    let private ExplainNoSettlement
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Put context (session, [Hued (Blue, Line "You have no settlement.")])

    let internal Explain
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Put context (session, [Line ""])
        match Settlement.GetSettlementForSession context session with
        | Some settlement ->
            ExplainSettlement context session settlement
        | None ->
            ExplainNoSettlement context session



