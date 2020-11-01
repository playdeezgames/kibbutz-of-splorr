namespace Splorr.Kibbutz.Presentation

open Splorr.Kibbutz.Business

type PresentationContext =
    inherit BusinessContext
    inherit Game.PollForCommandContext
    inherit Output.WriteContext
