namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module Settlement =
    let AbandonSettlementForSession = SettlementExistence.AbandonSettlementForSession
    let Explain = SettlementExplainer.Explain
    let HasSettlementForSession = SettlementExistence.HasSettlementForSession
    let StartSettlementForSession = SettlementExistence.StartSettlementForSession
    let Advance = SettlementAdvancer.Advance
            
            


            

    

