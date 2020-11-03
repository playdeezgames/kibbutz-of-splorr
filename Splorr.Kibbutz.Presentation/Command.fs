﻿namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open System
open Splorr.Kibbutz.Model

type Command =
    | AbandonSettlement
    | Advance
    | Assign of DwellerIdentifier * Assignment
    | Help
    | ListDwellers
    | Invalid of string
    | Quit
    | StartSettlement
