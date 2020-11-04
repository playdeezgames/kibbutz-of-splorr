namespace Splorr.Kibbutz.Business

open Splorr.Common

type BusinessContext = 
    inherit DwellerRepository.AssignToSessionContext
    inherit DwellerRepository.FindIdentifierForNameContext
    inherit DwellerRepository.GenerateIdentifierContext
    inherit DwellerRepository.GetBriefHistoryContext
    inherit DwellerRepository.GetContext
    inherit DwellerRepository.GetListForSessionContext
    inherit DwellerRepository.LogForDwellerContext
    inherit DwellerRepository.PurgeLogsForDwellerContext
    inherit DwellerRepository.PutContext
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

