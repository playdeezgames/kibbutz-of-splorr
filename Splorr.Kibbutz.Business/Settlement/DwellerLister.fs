namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal SettlementDwellerLister =
    let private DwellerToListItem
            (dweller : Dweller)
            : Message =
        Group
            [
                Hued (Light Magenta, Text (dweller.name |> sprintf "%-12s"))
                Hued (Blue, Text (" | "))
                Hued (Cyan, Text (dweller.sexGenes |> DwellerExplainer.ShortDescribeSexGenes |> sprintf "%-1s"))
                Hued (Blue, Text (" | "))
                Hued (Cyan, Text (dweller.location |> Location.ToString |> sprintf "%-8s"))
                Hued (Blue, Text (" | "))
                Hued (Cyan, Line (dweller.assignment |> Assignment.ToString |> sprintf "%-10s"))
            ]

    let private DwellerTableHeader =
        [
            Group
                [
                    Hued (Light Blue, Text ("Name" |> sprintf "%-12s"))
                    Hued (Blue, Text (" | "))
                    Hued (Light Blue, Text ("S" |> sprintf "%-1s"))
                    Hued (Blue, Text (" | "))
                    Hued (Light Blue, Text ("Location" |> sprintf "%-8s"))
                    Hued (Blue, Text (" | "))
                    Hued (Light Blue, Line ("Assignment" |> sprintf "%-10s"))
                ]
            Hued (Blue, Line "-------------+---+----------+-----------")
        ]

    let internal ListDwellers
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        DwellerRepository.GetDwellersForSession context session
        |> List.map DwellerToListItem
        |> List.append DwellerTableHeader
            
