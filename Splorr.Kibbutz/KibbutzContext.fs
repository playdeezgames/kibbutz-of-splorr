namespace Splorr.Kibbutz

open Splorr.Common
open System

type internal KibbutzContext() =
    interface CommonContext
    interface Output.WriteContext with
        member this.writeSink = ref Console.Write



