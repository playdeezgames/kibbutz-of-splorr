namespace Splorr.Kibbutz

open Splorr.Common
open System
open Splorr.Kibbutz.Presentation

type internal KibbutzContext() =
    interface CommonContext
    interface PresentationContext
    interface Output.WriteContext with
        member this.writeSink = ref OutputImplementation.Write
    interface Game.PollForCommandContext with
        member this.commandSource = 
            ref GameImplementation.PollForCommand
    interface Messages.GetContext with
        member this.sessionMessageSource = ref MessagesImplementation.Get
    interface Messages.PutContext with
        member this.sessionMessagesSink = ref MessagesImplementation.Put
    interface Messages.PurgeContext with
        member this.sessionMessagesPurge = ref MessagesImplementation.Purge
    interface Game.HandleInvalidCommandContext with
        member this.invalidCommandSink = ref GameImplementation.HandleInvalidCommand


