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
    interface Settlement.GetSettlementForSessionContext with
        member val settlementSource = ref (Fakes.Source ("Settlement.GetSettlementForSessionContext", None))
    interface Settlement.PutSettlementForSessionContext with
        member val settlementSink = ref (Fakes.Sink "Settlement.PutSettlementForSessionContext")
