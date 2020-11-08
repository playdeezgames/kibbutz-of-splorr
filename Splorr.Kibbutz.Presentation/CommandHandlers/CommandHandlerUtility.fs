namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module internal CommandHandlerUtility =
    let internal HandleStandardCommand
            (context : CommonContext)
            (session : SessionIdentifier)
            (message : Message)
            : SessionIdentifier option=
        Messages.Purge context session
        Messages.Put context session [Line ""]
        Messages.Put context session [ message ]
        Settlement.Explain context session
        Some session