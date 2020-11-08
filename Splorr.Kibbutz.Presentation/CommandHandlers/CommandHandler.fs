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
        | Assign (identifier, assignment) ->
            AssignCommandHandler.Handle context session identifier assignment
        | ExplainDweller identifier ->
            ExplainDwellerCommandHandler.Handle context session identifier
        | Help ->
            HelpCommandHandler.Handle context session
        | History (identifier, page) ->
            HistoryCommandHandler.Handle context session identifier page
        | Invalid text ->
            InvalidCommandHandler.Handle context text session
        | ListDwellers ->
            ListDwellersCommandHandler.Handle context session
        | Quit ->
            QuitCommandHandler.Handle context session
        | StartSettlement ->
            StartSettlementCommandHandler.Handle context session


