namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System


module Game = 
    type private Gamestate = Guid

    type Command =
        | Quit

    let Load
            (context : CommonContext)
            : Gamestate option =
        None

    let New
            (context: CommonContext)
            : Gamestate =
        Guid.NewGuid()

    let private UpdateDisplay
            (context : CommonContext)
            (gameState : Gamestate)
            : unit =
        Output.Write context "Hello, world!\n"

    type CommandSource = unit -> Command option
    type PollForCommandContext =
        abstract member commandSource : CommandSource ref
    let private PollForCommand
            (context : CommonContext)
            : Command option =
        (context :?> PollForCommandContext).commandSource.Value()

    let private HandleCommand
            (context : CommonContext)
            (command : Command)
            (gameState : Gamestate)
            : Gamestate option =
        None

    let private RunLoop
            (context : CommonContext)
            (gamestate : Gamestate)
            : Gamestate option =
        UpdateDisplay context gamestate
        match PollForCommand context with
        | Some command ->
            HandleCommand context command gamestate
        | _ ->
            Some gamestate

    let rec Run
            (context : CommonContext)
            (gamestate : Gamestate)
            : unit =
        match RunLoop context gamestate with
        | Some nextState ->
            Run context nextState
        | None ->
            ()


