namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerAssigner =
    let private AssignWhenDwellerDoesNotExistMessages = 
        [
            Hued (Red, Line "There is no such dweller in this settlement.")
        ]
        |> Group
    let private SuccessfulAssignmentOfDwellerMessages =
        [
            Hued (Green, Line "You update the dweller's assignment.")
        ]
        |> Group

    let private CompleteAssignmentOfDweller
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            (assignment:Assignment)
            : unit =
        let settlement =
            SettlementRepository.GetSettlementForSession context session
            |> Option.get
        DwellerLogRepository.LogForDweller
            context
            (identifier, settlement.turnCounter, Line (sprintf "Got assigned to %s" (assignment |> Assignment.ToString)))
        {dweller with 
            assignment = assignment}
        |> Some
        |> DwellerRepository.Put context identifier
    
    let internal Assign
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier: DwellerIdentifier)
            (assignment : Assignment)
            : Message =
        match DwellerSession.GetForSession context session identifier with
        | Some dweller ->
            CompleteAssignmentOfDweller context session identifier dweller assignment
            SuccessfulAssignmentOfDwellerMessages
        | _ ->
            AssignWhenDwellerDoesNotExistMessages
