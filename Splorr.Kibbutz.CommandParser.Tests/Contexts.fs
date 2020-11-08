module Contexts

open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Common
open System

type TestContext() =
    interface CommonContext
    interface BusinessContext
    interface Messages.GetContext with
        member val sessionMessageSource = ref (Fakes.Source ("Messages.GetContext", []))
    interface Messages.PurgeContext with
        member val sessionMessagesPurge = ref (Fakes.Sink "Messages.PurgeContext")
    interface Messages.PutContext with
        member val sessionMessagesSink = ref (Fakes.Sink "Messages.PutContext")
    interface SettlementRepository.GetSettlementForSessionContext with
        member val settlementSource = ref (Fakes.Source ("Settlement.GetSettlementForSessionContext", None))
    interface SettlementRepository.PutSettlementForSessionContext with
        member val settlementSink = ref (Fakes.Sink "Settlement.PutSettlementForSessionContext")
    interface DwellerRepository.GetListForSessionContext with
        member val sessionDwellerSource = ref (Fakes.Source ("DwellerRepository.GetListForSessionContext", []))
    interface DwellerRepository.GetContext with
        member val dwellerSingleSource = ref (Fakes.Source ("DwellerRepository.GetContext", None))
    interface DwellerRepository.AssignToSessionContext with
        member val dwellerSessionSink = ref (Fakes.Sink "DwellerRepository.AssignToSessionContext")
    interface DwellerRepository.PutContext with
        member val dwellerSingleSink = ref (Fakes.Sink "DwellerRepository.PutContext")
    interface DwellerRepository.GenerateIdentifierContext with
        member val dwellerIdentifierSource = ref (Fakes.Source ("DwellerRepository.GenerateIdentifierContext", Guid.Empty))
    interface SessionRepository.GenerateIdentifierContext with
        member val sessionIdentifierSource = ref (Fakes.Source ("SessionRepository.GenerateIdentifierContext", Guid.Empty))
    interface RandomUtility.RandomContext with
        member val random = ref null
    interface SessionRepository.ClearNamesContext with
        member val sessionNamePurger = ref (Fakes.Sink "SessionRepository.ClearNamesContext")
    interface SessionRepository.AddNameContext with
        member val sessionNameSink = ref (Fakes.Sink "SessionRepository.AddNameContext")
    interface SessionRepository.CheckNameContext with
        member val sessionNameValidator = ref (Fakes.Source ("SessionRepository.CheckNameContext", false))
    interface DwellerRepository.FindIdentifierForNameContext with
        member val dwellerIdentifierForNameSource = ref (Fakes.Source ("DwellerRepository.FindIdentifierForNameContext", None))
    interface DwellerLogRepository.LogForDwellerContext with
        member val dwellerLogSink = ref (Fakes.Sink "DwellerRepository.LogForDwellerContext")
    interface DwellerRepository.PurgeLogsForDwellerContext with
        member val dwellerLogPurger = ref (Fakes.Sink "DwellerRepository.PurgeLogsForDwellerContext")
    interface DwellerRepository.GetBriefHistoryContext with
        member val dwellerBriefHistorySource = ref (Fakes.Source ("DwellerRepository.GetBriefHistoryContext", []))
    interface DwellerRepository.GetPageHistoryContext with
        member val dwellerPageHistorySource = ref (Fakes.Source ("DwellerRepository.GetPageHistoryContext", []))

