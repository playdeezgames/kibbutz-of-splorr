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
    interface Settlement.GetSettlementForSessionContext with
        member this.settlementSource = ref SettlementImplementation.GetSettlementForSession
    interface Settlement.PutSettlementForSessionContext with
        member this.settlementSink = ref SettlementImplementation.PutSettlementForSession


