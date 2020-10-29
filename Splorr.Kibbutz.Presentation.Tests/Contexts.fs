module Contexts

open Splorr.Kibbutz.Presentation
open Splorr.Tests.Common
open Splorr.Common

type TestContext() =
    interface CommonContext
    interface PresentationContext
    interface Output.WriteContext with
        member val writeSink = ref (Fakes.Sink "Output.WriteContext")
    interface Game.PollForCommandContext with
        member val commandSource = ref (Fakes.Source ("Game.PollForCommandContext", None))