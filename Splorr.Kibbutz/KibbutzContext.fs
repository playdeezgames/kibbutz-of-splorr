namespace Splorr.Kibbutz

open Splorr.Common
open System
open Splorr.Kibbutz.Presentation

type internal KibbutzContext() =
    interface CommonContext
    interface PresentationContext
    interface Output.WriteContext with
        member this.writeSink = ref Console.Write
    interface Game.PollForCommandContext with
        member this.commandSource = 
            ref 
                (fun () -> 
                    Console.ReadLine() |> ignore
                    Some Game.Command.Quit)



