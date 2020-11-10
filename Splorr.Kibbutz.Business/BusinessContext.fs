namespace Splorr.Kibbutz.Business

open Splorr.Common

type BusinessContext = 
    inherit DwellerInventoryRepository.AddItemContext
    inherit DwellerInventoryRepository.GetPageContext
    inherit DwellerInventoryRepository.GetPageCountContext
    inherit DwellerInventoryRepository.PurgeItemsContext
    inherit DwellerHistoryRepository.GetBriefHistoryContext
    inherit DwellerHistoryRepository.GetPageCountContext
    inherit DwellerHistoryRepository.GetPageContext
    inherit DwellerHistoryRepository.AddHistoryContext
    inherit DwellerHistoryRepository.PurgeHistoryContext
    inherit DwellerRepository.AssignToSessionContext
    inherit DwellerRepository.FindIdentifierForNameContext
    inherit DwellerRepository.GenerateIdentifierContext
    inherit DwellerRepository.GetContext
    inherit DwellerRepository.GetListForSessionContext
    inherit DwellerRepository.PutContext
    inherit DwellerStatisticRepository.GetContext
    inherit DwellerStatisticRepository.PutContext
    inherit Messages.GetContext
    inherit Messages.PurgeContext
    inherit Messages.PutContext
    inherit RandomUtility.RandomContext
    inherit SessionRepository.AddNameContext
    inherit SessionRepository.ClearNamesContext
    inherit SessionRepository.CheckNameContext
    inherit SessionRepository.GenerateIdentifierContext
    inherit SettlementRepository.GetSettlementForSessionContext
    inherit SettlementRepository.PutSettlementForSessionContext

