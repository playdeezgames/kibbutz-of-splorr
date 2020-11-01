module Contexts

open Splorr.Common
open System

type TestContext () =
    interface CommonContext
    interface RandomUtility.RandomContext with
        member val random = ref null