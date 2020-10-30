namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

type Command =
    | AbandonSettlement
    | Help
    | Invalid of string
    | Quit
    | StartSettlement
