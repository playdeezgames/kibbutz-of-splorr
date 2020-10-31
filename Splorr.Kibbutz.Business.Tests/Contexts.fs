module Contexts

open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open Splorr.Common

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
