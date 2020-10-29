namespace Splorr.Kibbutz

open Splorr.Common
open System
open Splorr.Kibbutz.Presentation

type internal KibbutzContext() =
    interface CommonContext
    interface PresentationContext
    interface Output.WriteContext with
        member this.writeSink = ref Console.Write



