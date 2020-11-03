namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System
open Splorr.Kibbutz.Model

type Command =
    | AbandonSettlement
    | Advance
    | Assign of DwellerIdentifier * Assignment
    | ExplainDweller of DwellerIdentifier
    | Help
    | ListDwellers
    | Invalid of string
    | Quit
    | StartSettlement
