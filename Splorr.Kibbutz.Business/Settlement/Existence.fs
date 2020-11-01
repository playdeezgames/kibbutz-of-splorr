﻿namespace Splorr.Kibbutz.Business

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
            vowels = ["a";"e";"i";"o";"u"]
            consonants=["h";"k";"l";"m";"p"]
            nameLengthGenerator = 
                [
                    3, 1.0
                    4, 3.0
                    5, 6.0
                    6, 10.0
                    7, 12.0
                    8, 12.0
                    9, 10.0
                    10, 6.0
                    11, 3.0
                    12, 1.0
                ]
                |> Map.ofList
            nameStartGenerator = 
                [
                    true, 1.0
                    false, 1.0
                ]
                |> Map.ofList
        }

    let private SettlementAlreadyExistsMessages = 
        [
            Hued (Red, Line ("You already have a settlement!"))
        ]

    let private AbandonedSettlementMessages = 
        [
            Hued (Yellow, Line ("I'm sure those people will be fine and not starve to death or anything bad like that."))
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

    let private GenerateDweller
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        let dweller = 
            Dweller.Create context
        let identifier = DwellerRepository.GenerateIdentifier context
        DwellerRepository.Put context identifier (Some dweller)
        DwellerRepository.AssignToSession context session identifier

    let private GenerateDwellers
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        [1..3]
        |> List.iter
            (fun _ -> GenerateDweller context session)

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
        DwellerRepository.GetListForSession context session
        |> List.iter
            (fun identifier ->
                DwellerRepository.Put context identifier None
                DwellerRepository.RemoveFromSession context identifier)
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
