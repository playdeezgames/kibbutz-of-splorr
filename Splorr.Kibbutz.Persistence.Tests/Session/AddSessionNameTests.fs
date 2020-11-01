module SessionNames.AddTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence

[<Test>]
let ``AddName.It clears out the session names.`` () =
    SessionNamesStore.ClearNames Dummies.ValidSessionIdentfier
    SessionNamesStore.AddName (Dummies.ValidSessionIdentfier, "name")
    let actual = SessionNamesStore.HasName (Dummies.ValidSessionIdentfier, "name")
    Assert.IsTrue(actual)
