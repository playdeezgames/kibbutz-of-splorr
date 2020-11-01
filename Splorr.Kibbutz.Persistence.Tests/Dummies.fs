module Dummies

open System
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model

let ValidSessionIdentfier : SessionIdentifier = Guid.NewGuid()
let ValidDwellerIdentifier : DwellerIdentifier = Guid.NewGuid().ToString()