namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module internal CommandHandler =
    let internal HandleCommand
            (context : CommonContext)
            (command : Command)
            (session : SessionIdentifier)
            : SessionIdentifier option =
        match command with
        | AbandonSettlement ->
            AbandonSettlementCommandHandler.Handle context session
        | Advance ->
            AdvanceCommandHandler.Handle context session
        | Help ->
            HelpCommandHandler.Handle context session
        | Invalid text ->
            InvalidCommandHandler.Handle context text session
        | Quit ->
            QuitCommandHandler.Handle context session
        | StartSettlement ->
            StartSettlementCommandHandler.Handle context session


