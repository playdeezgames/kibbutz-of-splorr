module Settlement.GetForSessionTests


open NUnit.Framework
open Splorr.Kibbutz.Persistence
open Splorr.Kibbutz.Business

[<Test>]
let ``GetSettlementForSession.It retrieves the current settlement for the given session.`` () =
    SettlementImplementation.PutSettlementForSession(Dummies.ValidSessionIdentfier, Some { iExistOnlyToHaveAFieldInTheRecord=0})
    let  actual = SettlementImplementation.GetSettlementForSession Dummies.ValidSessionIdentfier
    Assert.AreEqual(Some { iExistOnlyToHaveAFieldInTheRecord=0}, actual)
