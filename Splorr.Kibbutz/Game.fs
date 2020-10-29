namespace Splorr.Kibbutz

open Splorr.Common
open System


module Game = 
    type private Gamestate = Guid

    type private Command =
        | Quit

    let internal Load
            (context : CommonContext)
            : Gamestate option =
        None

    let internal New
            (context: CommonContext)
            : Gamestate =
        Guid.NewGuid()

    let private UpdateDisplay
            (context : CommonContext)
            (gameState : Gamestate)
            : unit =
        Output.Write context "Hello, world!\n"

    let private PollForCommand
            (context : CommonContext)
            : Command option =
        Console.ReadLine() |> ignore
        Some Quit

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

    let rec internal Run
            (context : CommonContext)
            (gamestate : Gamestate)
            : unit =
        match RunLoop context gamestate with
        | Some nextState ->
            Run context nextState
        | None ->
            ()


