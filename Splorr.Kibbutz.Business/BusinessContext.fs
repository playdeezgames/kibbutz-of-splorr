namespace Splorr.Kibbutz.Business

type BusinessContext = 
    inherit Messages.GetContext
    inherit Messages.PurgeContext
    inherit Messages.PutContext
    inherit Settlement.GetSettlementForSessionContext
    inherit Settlement.PutSettlementForSessionContext

