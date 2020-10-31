﻿namespace Splorr.Kibbutz.Business

type BusinessContext = 
    inherit DwellerRepository.GetContext
    inherit DwellerRepository.GetListForSessionContext
    inherit Messages.GetContext
    inherit Messages.PurgeContext
    inherit Messages.PutContext
    inherit SettlementRepository.GetSettlementForSessionContext
    inherit SettlementRepository.PutSettlementForSessionContext

