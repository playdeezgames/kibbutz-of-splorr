module Dummies

open Splorr.Kibbutz.Business
open System

let internal ValidSessionIdentifier : SessionIdentifier = Guid.NewGuid()