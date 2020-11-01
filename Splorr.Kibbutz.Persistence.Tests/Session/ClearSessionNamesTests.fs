module SessionName.ClearTests

open NUnit.Framework
open Splorr.Kibbutz.Persistence

[<Test>]
let ``ClearNames.It clears out the session names.`` () =
    SessionNamesStore.AddName (Dummies.ValidSessionIdentfier, "name")
    SessionNamesStore.ClearNames Dummies.ValidSessionIdentfier
    let actual = SessionNamesStore.HasName (Dummies.ValidSessionIdentfier, "name")
    Assert.IsFalse(actual)
    

