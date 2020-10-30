namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

module internal Explainer =
    let internal Explain
            (context : CommonContext)
            (session : SessionIdentifier)
            : unit =
        Messages.Put context (session, [Line ""])
        Messages.Put context (session, [Line "(the current state of the game will be explained here)"])



