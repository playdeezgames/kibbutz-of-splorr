namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System

module internal CommandHandlerUtility =
    let internal HandleStandardCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            (messages : Message list)
            : SessionIdentifier option=
        Messages.Purge context session
        Messages.Put context session [Line ""]
        Messages.Put context session messages
        Settlement.Explain context session
        Some session