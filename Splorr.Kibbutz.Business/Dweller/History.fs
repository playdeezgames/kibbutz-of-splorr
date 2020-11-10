namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerHistory =
    let internal History
            (context : CommonContext)
            (session : SessionIdentifier)
            (identifier : DwellerIdentifier)
            (page : uint64)
            : Message =
        DwellerPageGenerator.Generate
            context
            session
            "history"
            (DwellerHistoryRepository.GetPageCount context)
            (DwellerHistoryRepository.GetPage context)
            DwellerExplainer.RenderHistoryAsMessage
            identifier
            page


