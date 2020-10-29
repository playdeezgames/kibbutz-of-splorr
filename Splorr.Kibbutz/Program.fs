open System
open Splorr.Common
open Splorr.Kibbutz

[<EntryPoint>]
let main argv =
    let context = KibbutzContext() 

    Game.Load context
    |> Option.defaultValue (Game.New context)
    |> Game.Run context

    0
