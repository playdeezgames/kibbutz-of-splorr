module Settlement.PutForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

[<Test>]
let ``PutSettlementForSession.It replaces the value for the settlement for a given session.`` () =
    SettlementStore.PutSettlementForSession(Dummies.ValidSessionIdentfier, Some Dummies.ValidSettlement)
    SettlementStore.PutSettlementForSession(Dummies.ValidSessionIdentfier, None)
    let  actual = SettlementStore.GetSettlementForSession Dummies.ValidSessionIdentfier
    Assert.AreEqual(None, actual)
