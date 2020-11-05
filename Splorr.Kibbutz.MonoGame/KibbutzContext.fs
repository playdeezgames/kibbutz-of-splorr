namespace Splorr.Kibbutz.Monogame

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
        member this.sessionDwellerSource = ref DwellerIdentifierStore.GetListForSession
    interface DwellerRepository.GetContext with
        member this.dwellerSingleSource = ref DwellerStore.Get
    interface DwellerRepository.AssignToSessionContext with
        member this.dwellerSessionSink = ref DwellerIdentifierStore.AssignToSession
    interface DwellerRepository.PutContext with
        member this.dwellerSingleSink = ref DwellerStore.Put
    interface DwellerRepository.GenerateIdentifierContext with
        member this.dwellerIdentifierSource = ref DwellerIdentifierStore.GenerateIdentifier
    interface SessionRepository.GenerateIdentifierContext with
        member this.sessionIdentifierSource = ref SessionIdentifierStore.GenerateIdentifier
    interface RandomUtility.RandomContext with
        member this.random = ref (Random())
    interface SessionRepository.ClearNamesContext with
        member this.sessionNamePurger = ref SessionNamesStore.ClearNames
    interface SessionRepository.AddNameContext with
        member this.sessionNameSink = ref SessionNamesStore.AddName
    interface SessionRepository.CheckNameContext with
        member this.sessionNameValidator = ref SessionNamesStore.HasName
    interface DwellerRepository.FindIdentifierForNameContext with
        member this.dwellerIdentifierForNameSource = ref DwellerStore.FindIdentifierForName
    interface DwellerRepository.LogForDwellerContext with
        member this.dwellerLogSink = ref DwellerLogStore.LogForDweller
    interface DwellerRepository.PurgeLogsForDwellerContext with
        member this.dwellerLogPurger = ref DwellerLogStore.PurgeLogsForDweller
    interface DwellerRepository.GetBriefHistoryContext with
        member this.dwellerBriefHistorySource = ref DwellerLogStore.GetBriefHistory
