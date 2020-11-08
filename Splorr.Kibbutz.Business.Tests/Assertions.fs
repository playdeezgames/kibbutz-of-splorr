module Assertions

open NUnit.Framework
open Splorr.Kibbutz.Model

let ValidateMessageIsGroupWithGivenItemCount
        (actual : Message, itemCount: int)
        : unit =
    match actual with 
    | Group items ->
        Assert.AreEqual(itemCount, items.Length)
    | x ->
        Assert.Fail(sprintf "Expected a group with %d subitems, but instead found '%A'" itemCount x)
