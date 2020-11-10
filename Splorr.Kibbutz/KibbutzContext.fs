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
    interface DwellerHistoryRepository.AddHistoryContext with
        member this.dwellerLogSink = ref DwellerHistoryStore.LogForDweller
    interface DwellerHistoryRepository.PurgeHistoryContext with
        member this.dwellerLogPurger = ref DwellerHistoryStore.PurgeLogsForDweller
    interface DwellerHistoryRepository.GetBriefHistoryContext with
        member this.dwellerBriefHistorySource = ref DwellerHistoryStore.GetBriefHistory
    interface DwellerHistoryRepository.GetPageContext with
        member this.dwellerPageHistorySource = ref DwellerHistoryStore.GetPageHistory
    interface DwellerHistoryRepository.GetPageCountContext with
        member this.dwellerPageCountHistorySource = ref DwellerHistoryStore.GetHistoryPageCount
    interface DwellerInventoryRepository.AddItemContext with
        member this.dwellerInventoryAdder = ref DwellerInventoryStore.AddItem
    interface DwellerInventoryRepository.PurgeItemsContext with
        member this.dwellerInventoryPurger = ref DwellerInventoryStore.PurgeItems
    interface DwellerInventoryRepository.GetPageContext with
        member this.dwellerInventoryPageSource = ref DwellerInventoryStore.GetPage
    interface DwellerInventoryRepository.GetPageCountContext with
        member this.dwellerInventoryPageCountSource = ref DwellerInventoryStore.GetPageCount
    interface DwellerStatisticRepository.GetContext with
        member this.dwellerStatisticSource = raise (System.NotImplementedException())
    interface DwellerStatisticRepository.PutContext with
        member this.dwellerStatisticSink = raise (System.NotImplementedException())
