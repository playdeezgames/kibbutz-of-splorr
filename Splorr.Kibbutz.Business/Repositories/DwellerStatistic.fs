namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerStatisticRepository =
    type DwellerStatisticSource = DwellerIdentifier * DwellerStatisticIdentifier -> DwellerStatistic option
    type GetContext =
        abstract member dwellerStatisticSource : DwellerStatisticSource ref
    let internal Get
            (context : CommonContext) =
        (context :?> GetContext).dwellerStatisticSource.Value

    type DwellerStatisticSink = DwellerIdentifier * DwellerStatisticIdentifier * DwellerStatistic option -> unit
    type PutContext =
        abstract member dwellerStatisticSink : DwellerStatisticSink ref
    let internal Put
            (context : CommonContext) =
        (context :?> PutContext).dwellerStatisticSink.Value

    type DwellerStatisticPurger = DwellerIdentifier -> unit
    type PurgeContext =
        abstract member dwellerStatisticPurger : DwellerStatisticPurger ref
    let internal Purge
            (context : CommonContext) =
        (context :?> PurgeContext).dwellerStatisticPurger.Value

