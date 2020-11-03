namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerAssigner =
    let private AssignWhenDwellerDoesNotExistMessages = 
        [
            Hued (Red, Line "There is no such dweller in this settlement.")
        ]
    let private SuccessfulAssignmentOfDwellerMessages =
        [
            Hued (Green, Line "You update the dweller's assignment.")
        ]

    let private CompleteAssignmentOfDweller
            (context : CommonContext)
            (identifier : DwellerIdentifier)
            (dweller : Dweller)
            (assignment:Assignment)
            : unit =
        {dweller with 
            assignment = assignment}
        |> Some
        |> DwellerRepository.Put context identifier
    
    let internal Assign
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier: DwellerIdentifier)
            (assignment : Assignment)
            : Message list =
        match DwellerRepository.GetForSession context session identifier with
        | Some dweller ->
            CompleteAssignmentOfDweller context identifier dweller assignment
            SuccessfulAssignmentOfDwellerMessages
        | _ ->
            AssignWhenDwellerDoesNotExistMessages
