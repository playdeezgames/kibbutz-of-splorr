module RandomUtility.GenerateFromWeightedValuesTests

open NUnit.Framework
open Splorr.Common
open System

[<Test>]
let ``GenerateFromWeightedValues.It generates a value from a weighted set of values.`` () =
    let context = Contexts.TestContext()
    (context :> RandomUtility.RandomContext).random := Random(0)
    let candidates = 
        [
            "1", 1.0
            "2", 1.0
            "3", 1.0
        ]
        |> Map.ofList
    let actual = RandomUtility.GenerateFromWeightedValues context candidates
    Assert.AreEqual(Some "3", actual)