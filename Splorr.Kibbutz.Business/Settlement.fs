namespace Splorr.Kibbutz.Business

open Splorr.Common
open System

type Settlement = 
    {
        turnCounter : uint64
    }

module Settlement =
    type SettlementSource = SessionIdentifier -> Settlement option
    type GetSettlementForSessionContext =
        abstract member settlementSource : SettlementSource ref
    let private GetSettlementForSession
            (context : CommonContext) =
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
            (settlement : Settlement)
            : unit =
        Messages.Put context session [Line (sprintf "Turn# %u" settlement.turnCounter)]

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
        match GetSettlementForSession context session with
        | Some settlement ->
            ExplainSettlement context session settlement
        | _ ->
            ExplainNoSettlement context session

    let Advance
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        match GetSettlementForSession context session with
        | Some settlement ->
            PutSettlementForSession context session (Some {settlement with turnCounter = settlement.turnCounter + 1UL})
            [Hued (Green, Line "You advance your settlement to the next turn.")]
        | _ ->
            [Hued (Red, Line "You have no settlement to advance.")]
            
            


            

    

