module Settlement.GetForSessionTests


open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``GetSettlementForSession.It retrieves the current settlement for the given session.`` () =
    SettlementStore.PutSettlementForSession(Dummies.ValidSessionIdentfier, Some { turnCounter=0UL})
    let  actual = SettlementStore.GetSettlementForSession Dummies.ValidSessionIdentfier
    Assert.AreEqual(Some { turnCounter=0UL}, actual)
