namespace Splorr.Kibbutz

open Splorr.Common
open System
open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Persistence

type internal KibbutzContext() =
    interface CommonContext
    interface PresentationContext
    interface Output.WriteContext with
        member this.writeSink = ref OutputImplementation.Write
    interface Game.PollForCommandContext with
        member this.commandSource = 
            ref GameImplementation.PollForCommand
    interface Messages.GetContext with
        member this.sessionMessageSource = ref MessagesStore.Get
    interface Messages.PutContext with
        member this.sessionMessagesSink = ref MessagesStore.Put
    interface Messages.PurgeContext with
        member this.sessionMessagesPurge = ref MessagesStore.Purge
    interface SettlementRepository.GetSettlementForSessionContext with
        member this.settlementSource = ref SettlementStore.GetSettlementForSession
    interface SettlementRepository.PutSettlementForSessionContext with
        member this.settlementSink = ref SettlementStore.PutSettlementForSession
    interface DwellerRepository.GetListForSessionContext with
        member this.sessionDwellerSource = ref DwellerStore.GetListForSession
    interface DwellerRepository.GetContext with
        member this.dwellerSingleSource = ref DwellerStore.Get
    interface DwellerRepository.AssignToSessionContext with
        member this.dwellerSessionSink = ref DwellerStore.AssignToSession
    interface DwellerRepository.PutContext with
        member this.dwellerSingleSink = ref DwellerStore.Put


