namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

type Command =
    | AbandonSettlement
    | Advance
    | Help
    | Invalid of string
    | Quit
    | StartSettlement
