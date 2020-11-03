namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module Settlement =
    let AbandonSettlementForSession = SettlementExistence.AbandonSettlementForSession
    let Advance = SettlementAdvancer.Advance
    let Explain = SettlementExplainer.Explain
    let HasSettlementForSession = SettlementExistence.HasSettlementForSession
    let StartSettlementForSession = SettlementExistence.StartSettlementForSession

    let ListDwellers
            (context : CommonContext)
            (session : SessionIdentifier)
            : Message list =
        DwellerRepository.GetListForSession context session
        |> List.map
            (DwellerRepository.Get context >> Option.get)
        |> List.fold
            (fun messages dweller ->
                List.append 
                    messages
                    [
                        Group
                            [
                                Hued (Light Magenta, Text (dweller.name |> sprintf "%-12s"))
                                Hued (Blue, Text (" | "))
                                Hued (Cyan, Text (dweller.sexGenes |> Dweller.ShortDescribeSexGenes |> sprintf "%-1s"))
                                Hued (Blue, Text (" | "))
                                Hued (Cyan, Text (dweller.location |> Location.ToString |> sprintf "%-8s"))
                                Hued (Blue, Text (" | "))
                                Hued (Cyan, Line (dweller.assignment |> Assignment.ToString |> sprintf "%-10s"))
                            ]
                    ]) []
        |> List.append
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


            
            


            

    

