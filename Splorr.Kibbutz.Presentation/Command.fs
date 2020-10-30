namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System

type Command =
    | Quit
    | Invalid of string
