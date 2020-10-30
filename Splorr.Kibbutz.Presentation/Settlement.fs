namespace Splorr.Kibbutz.Presentation

open Splorr.Common

type Settlement = 
    {
        iExistOnlyToHaveAFieldInTheRecord : int
    }

module Settlement =
    type SettlementSource = SessionIdentifier -> Settlement option
    type GetSettlementForSessionContext =
        abstract member settlementSource : SettlementSource ref
    let internal GetSettlementForSession
            (context : CommonContext)=
        (context :?> GetSettlementForSessionContext).settlementSource.Value
    

