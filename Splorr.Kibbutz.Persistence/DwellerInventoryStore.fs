namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System

module DwellerInventoryStore =
    let private inventories : Map<DwellerIdentifier, Item list> ref = ref Map.empty
    let private PageLength = 10

    let AddItem
            (identifier : DwellerIdentifier, item: Item) : unit =
        let oldInventory =
            inventories.Value
            |> Map.tryFind identifier
            |> Option.defaultValue []
        let newInventory =
            List.append oldInventory [ item ]
        inventories :=
            inventories.Value
            |> Map.add identifier newInventory

    let PurgeItems
            (identifier: DwellerIdentifier) : unit =
        inventories :=
            inventories.Value
            |> Map.remove identifier

    let GetPage
            (identifier: DwellerIdentifier, page : uint64) : Item list =
        inventories.Value
        |> Map.tryFind identifier
        |> PageUtility.GetPage PageLength page 
            
    let GetPageCount
            (identifier : DwellerIdentifier)
            : uint64 =
        inventories.Value 
        |> Map.tryFind identifier
        |> PageUtility.GetPageCount PageLength

        

