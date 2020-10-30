namespace Splorr.Kibbutz.Presentation

open Splorr.Common

type Settlement = 
    {
        iExistOnlyToHaveAFieldInTheRecord : int
    }

module Settlement =
    type SettlementSource = SessionIdentifier -> Settlement option
    type GetSettlementForSessionContext =
        abstract member settlementSource : SettlementSource ref
    let internal GetSettlementForSession
            (context : CommonContext)=
        (context :?> GetSettlementForSessionContext).settlementSource.Value

    type SettlementSink = SessionIdentifier * Settlement option -> unit
    type PutSettlementForSessionContext =
        abstract member settlementSink : SettlementSink ref
    let private PutSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            (settlement : Settlement option)
            : unit =
        (context :?> PutSettlementForSessionContext).settlementSink.Value (session, settlement)

    let private GenerateSettlement
            (context : CommonContext)
            : Settlement =
        {
            iExistOnlyToHaveAFieldInTheRecord = 0
        }

    let internal StartSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        match GetSettlementForSession context session with
        | Some _ ->
            [
                Hued (Red, Line ("You already have a settlement!"))
            ]
        | None ->
            GenerateSettlement context
            |> Some
            |> PutSettlementForSession context session
            [
                Hued (Green, Line ("You start a new settlement!"))
            ]

    

