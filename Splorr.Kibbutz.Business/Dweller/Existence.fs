namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerExistence =
    let internal Abandon 
            (context : CommonContext) 
            (identifier : DwellerIdentifier)
            : unit =
        DwellerInventoryRepository.PurgeItems context identifier
        DwellerHistoryRepository.PurgeHistory context identifier
        DwellerStatisticRepository.Purge context identifier
        DwellerRepository.Put context identifier None
        DwellerRepository.RemoveFromSession context identifier


