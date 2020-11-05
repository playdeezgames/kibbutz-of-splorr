open System
open Splorr.Kibbutz.Monogame

[<EntryPoint>]
let main argv =
    let context = KibbutzContext()
    use game = new HostGame(context)
    game.Run()
    0
