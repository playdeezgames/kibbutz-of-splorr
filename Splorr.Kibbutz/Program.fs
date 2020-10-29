open System
open Splorr.Common
open Splorr.Kibbutz
open Splorr.Kibbutz.Presentation

[<EntryPoint>]
let main argv =
    let context = KibbutzContext() 

    Game.Load context
    |> Option.defaultValue (Game.New context)
    |> Game.Run context

    0
