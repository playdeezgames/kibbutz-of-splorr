namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module Dweller =
    let Assign = DwellerAssigner.Assign
    let Explain = DwellerExplainer.Explain
    let FindIdentifierForName = DwellerRepository.FindIdentifierForName
    let History = DwellerHistory.History

