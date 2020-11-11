namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal DwellerStatistic =
    let private GetDefault
            (statistic : DwellerStatisticIdentifier)
            : DwellerStatistic =
        0.0

    let private ClampFatigue
            (value : DwellerStatistic)
            : DwellerStatistic =
        max value 0.0

    let private Clamp
            (statistic: DwellerStatisticIdentifier)
            (value : DwellerStatistic) =
        match statistic with
        | Fatigue ->
            ClampFatigue value
        | _ ->
            value

    let private Get
            (context : CommonContext)
            (statistic : DwellerStatisticIdentifier)
            (identifier : DwellerIdentifier)
            : DwellerStatistic =
        DwellerStatisticRepository.Get context (identifier, statistic)
        |> Option.map (Clamp statistic)
        |> Option.defaultValue (GetDefault statistic)

    let internal ChangeBy
            (context : CommonContext)
            (identifier : DwellerIdentifier)
            (statistic : DwellerStatisticIdentifier)
            (amount : DwellerStatistic)
            : unit =
        let value = 
            Get context statistic identifier
        DwellerStatisticRepository.Put context (identifier, statistic, Some (value + amount))

    let internal GetHunger
            (context : CommonContext) =
        Get context Hunger

    let internal GetFatigue
            (context : CommonContext) =
        Get context Fatigue

    let private dwellerStatisticTemplates : Map<DwellerStatisticIdentifier, DwellerStatistic> =
        [
            Hunger, 0.0
            Fatigue, 0.0
        ]
        |> Map.ofList

    let internal InitializeStatistics
            (context : CommonContext)
            (identfier : DwellerIdentifier)
            : unit =
        dwellerStatisticTemplates
        |> Map.iter
            (fun statistic value ->
                DwellerStatisticRepository.Put context (identfier, statistic, Some value))



