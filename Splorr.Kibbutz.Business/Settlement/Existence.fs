namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal SettlementExistence =
    let internal HasSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : bool =
        SettlementRepository.GetSettlementForSession context session
        |> Option.isSome

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

    let private GenerateDwellers
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        let dweller = 
            Dweller.Create context
        let identifier =
            Guid.NewGuid()//TODO : hide that this is a guid?
        DwellerRepository.Put context identifier (Some dweller)
        DwellerRepository.AssignToSession context session identifier
            

    let private GenerateAndPutNewSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list = 
        GenerateSettlement context
        |> Some
        |> SettlementRepository.PutSettlementForSession context session
        GenerateDwellers context session
        SettlementStartedMessages 

    let internal StartSettlementForSession
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
        SettlementRepository.PutSettlementForSession context session None
        AbandonedSettlementMessages

    let internal AbandonSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        if HasSettlementForSession context session then
            ActuallyAbandonSettlementForSession context session
        else
            NoSettlementExistsMessages
