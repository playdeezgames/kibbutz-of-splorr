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

    let internal HasSettlementForSession
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
            iExistOnlyToHaveAFieldInTheRecord = 0
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

    let internal StartSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        if HasSettlementForSession context session then
            SettlementAlreadyExistsMessages   
        else
            GenerateAndPutNewSettlementForSession context session

    let internal ActuallyAbandonSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        PutSettlementForSession context session None
        AbandonedSettlementMessages

    let internal AbandonSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        if HasSettlementForSession context session then
            ActuallyAbandonSettlementForSession context session
        else
            NoSettlementExistsMessages
            


            

    

