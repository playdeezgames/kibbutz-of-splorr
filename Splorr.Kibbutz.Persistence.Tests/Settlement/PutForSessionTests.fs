module Settlement.PutForSessionTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business

[<Test>]
let ``PutSettlementForSession.It replaces the value for the settlement for a given session.`` () =
    SettlementImplementation.PutSettlementForSession(Dummies.ValidSessionIdentfier, Some { iExistOnlyToHaveAFieldInTheRecord=0})
    SettlementImplementation.PutSettlementForSession(Dummies.ValidSessionIdentfier, None)
    let  actual = SettlementImplementation.GetSettlementForSession Dummies.ValidSessionIdentfier
    Assert.AreEqual(None, actual)
