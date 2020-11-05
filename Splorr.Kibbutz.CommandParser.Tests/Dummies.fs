module Dummies

open Splorr.Kibbutz.Model
open Splorr.Kibbutz.Business
open System

let internal ValidSessionIdentifier : SessionIdentifier = Guid.NewGuid()
let internal ValidDwellerIdentifier : DwellerIdentifier = Guid.NewGuid()