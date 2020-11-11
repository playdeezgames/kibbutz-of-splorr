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
            vowels = ["a";"e";"i";"o";"u"] |> List.map (fun l -> (l, 1.0)) |> Map.ofList
            consonants=["h";"k";"l";"m";"p"] |> List.map (fun l -> (l, 1.0)) |> Map.ofList
            nameLengthGenerator = 
                [
                    3, 16.0
                    4, 64.0
                    5, 256.0
                    6, 256.0
                    7, 64.0
                    8, 16.0
                    9, 8.0
                    10, 4.0
                    11, 2.0
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
        |> Group

    let private AbandonedSettlementMessages = 
        [
            Hued (Yellow, Line ("I'm sure those people will be fine and not starve to death or anything bad like that."))
            Hued (Green, Line ("You abandon yer settlement!"))
        ]
        |> Group

    let private NoSettlementExistsMessages = 
        [
            Hued (Red, Line ("You don't have a settlement!"))
        ]
        |> Group

    let private SettlementStartedMessages =
        [
            Hued (Green, Line ("You start a new settlement!"))
        ]
        |> Group

    let private ToVowelOrConsonantFlag
            (nameStart : bool)
            (namePosition : int)
            : bool =
        if namePosition % 2 = 1 then 
            nameStart 
        else 
            not nameStart

    let private FromFlagToTable
            (trueTable : Map<string, float>)
            (falseTable : Map<string, float>)
            (flag: bool)
            : Map<string, float> =
        if flag then
            trueTable
        else
            falseTable

    let rec private GenerateDwellerName
            (context : CommonContext)
            (session: SessionIdentifier)
            (settlement : Settlement)
            : string =
        let nameLength = 
            RandomUtility.GenerateFromWeightedValues context settlement.nameLengthGenerator
        let nameStart =
            RandomUtility.GenerateFromWeightedValues context settlement.nameStartGenerator
        let candidate =
            [1..nameLength]
            |> List.map
                (ToVowelOrConsonantFlag nameStart 
                >> FromFlagToTable settlement.vowels settlement.consonants 
                >> RandomUtility.GenerateFromWeightedValues context)
            |> List.reduce (+)
        if SessionRepository.HasName context session candidate then
            GenerateDwellerName context session settlement
        else
            SessionRepository.AddName context session candidate
            candidate


    let private GenerateDweller
            (context : CommonContext)
            (session : SessionIdentifier)
            (settlement : Settlement)
            : unit =
        let dweller = 
            GenerateDwellerName context session settlement
            |> DwellerCreator.Create context
        let identifier = DwellerRepository.GenerateIdentifier context
        DwellerRepository.Put context identifier (Some dweller)
        DwellerHistoryRepository.AddHistory context (identifier, settlement.turnCounter, Line "Came into being.")
        DwellerRepository.AssignToSession context session identifier

    let private GenerateDwellers
            (context : CommonContext)
            (session : SessionIdentifier)
            (settlement : Settlement)
            : unit =
        [1..3]
        |> List.iter
            (fun _ -> GenerateDweller context session settlement)

    let private GenerateAndPutNewSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message = 
        SessionRepository.ClearNames context session
        let settlement = 
            GenerateSettlement context
        settlement
        |> Some
        |> SettlementRepository.PutSettlementForSession context session
        GenerateDwellers context session settlement
        SettlementStartedMessages 

    let internal StartSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message =
        if HasSettlementForSession context session then
            SettlementAlreadyExistsMessages   
        else
            GenerateAndPutNewSettlementForSession context session

    let private AbandonDwellers 
            (context : CommonContext)
            (session : SessionIdentifier) 
            : unit = 
        DwellerRepository.GetListForSession context session
        |> List.iter
            (DwellerCreator.Abandon context)


    let private ActuallyAbandonSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message =
        AbandonDwellers context session
        SettlementRepository.PutSettlementForSession context session None
        AbandonedSettlementMessages

    let internal AbandonSettlementForSession
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message =
        if HasSettlementForSession context session then
            ActuallyAbandonSettlementForSession context session
        else
            NoSettlementExistsMessages
