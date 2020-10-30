namespace Splorr.Kibbutz.Presentation

type PresentationContext =
    inherit Game.PollForCommandContext
    inherit Messages.GetContext
    inherit Messages.PurgeContext
    inherit Messages.PutContext
    inherit Output.WriteContext