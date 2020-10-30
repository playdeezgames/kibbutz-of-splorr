namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

type Command =
    | Help
    | Invalid of string
    | Quit
