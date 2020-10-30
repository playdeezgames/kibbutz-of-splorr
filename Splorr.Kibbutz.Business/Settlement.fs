namespace Splorr.Kibbutz.Business

open Splorr.Common

type Settlement = 
    {
        turnCounter : uint64
    }

module Settlement =
    type SettlementSource = SessionIdentifier -> Settlement option
    type GetSettlementForSessionContext =
        abstract member settlementSource : SettlementSource ref
    let private GetSettlementForSession
            (context : CommonContext)=
        (context :?> GetSettlementForSessionContext).settlementSource.Value

    let HasSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : bool =
        (GetSettlementForSession context session).IsSome

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
            turnCounter = 0UL
        }

    let private SettlementAlreadyExistsMessages = 
        [
            Hued (Red, Line ("You already have a settlement!"))
        ]

    let private AbandonedSettlementMessages = 
        [
            Hued (Green, Line ("You abandon yer settlement!"))
        ]


    let private NoSettlementExistsMessages = 
        [
            Hued (Red, Line ("You don't have a settlement!"))
        ]

    let private SettlementStartedMessages =
        [
            Hued (Green, Line ("You start a new settlement!"))
        ]

    let private GenerateAndPutNewSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list = 
        GenerateSettlement context
        |> Some
        |> PutSettlementForSession context session
        SettlementStartedMessages 

    let StartSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        if HasSettlementForSession context session then
            SettlementAlreadyExistsMessages   
        else
            GenerateAndPutNewSettlementForSession context session

    let private ActuallyAbandonSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        PutSettlementForSession context session None
        AbandonedSettlementMessages

    let AbandonSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        if HasSettlementForSession context session then
            ActuallyAbandonSettlementForSession context session
        else
            NoSettlementExistsMessages

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

    let Explain
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Put context session [Line ""]
        if HasSettlementForSession context session then
            ExplainSettlement context session
        else
            ExplainNoSettlement context session
            


            

    

