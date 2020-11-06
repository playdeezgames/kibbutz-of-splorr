module RandomUtility.PickFromListRandomlyTests

open NUnit.Framework
open Splorr.Common
open System

[<Test>]
let ``PickFromListRandomly.It picks from a list randomly.`` () = 
    let context = Contexts.TestContext()
    (context :> RandomUtility.RandomContext).random := Random(0)
    let candidates = 
        [
            "1"
            "2"
            "3"
        ]
    let actual = RandomUtility.PickFromListRandomly context candidates
    Assert.AreEqual("1", actual)


