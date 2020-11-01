module GameNewTests

open NUnit.Framework
open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Tests.Common
open System

[<Test>]
let ``New.It generates a new session.`` () =
    let calledPurgeMessages = ref false
    let calledPutMessages = ref false
    let calledGetSettlementForSession = ref false
    let calledGenerateSessionIdentifier = ref false
    let context = Contexts.TestContext()
    (context :> Messages.PurgeContext).sessionMessagesPurge := Spies.Sink(calledPurgeMessages)
    (context :> Messages.PutContext).sessionMessagesSink := Spies.Sink(calledPutMessages)
    (context :> SettlementRepository.GetSettlementForSessionContext).settlementSource := Spies.Source(calledGetSettlementForSession, None)
    (context :> SessionRepository.GenerateIdentifierContext).sessionIdentifierSource := Spies.Source(calledGenerateSessionIdentifier, Guid.NewGuid())
    let actual = Game.New context
    Assert.AreNotEqual(Guid.Empty, actual)
    Assert.IsTrue(calledPurgeMessages.Value)
    Assert.IsTrue(calledPutMessages.Value)
    Assert.IsTrue(calledGetSettlementForSession.Value)
    Assert.IsTrue(calledGenerateSessionIdentifier.Value)
    

