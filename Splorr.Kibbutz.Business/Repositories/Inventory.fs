namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module DwellerInventoryRepository =
    type DwellerInventoryAdder = DwellerIdentifier * Item -> unit
    type AddItemContext =
        abstract member dwellerInventoryAdder : DwellerInventoryAdder ref
    let internal AddItem
            (context : CommonContext) =
        (context :?> AddItemContext).dwellerInventoryAdder.Value

    type DwellerInventoryPurger = DwellerIdentifier -> unit
    type PurgeItemsContext =
        abstract member dwellerInventoryPurger : DwellerInventoryPurger ref
    let internal PurgeItems
            (context : CommonContext) =
        (context :?> PurgeItemsContext).dwellerInventoryPurger.Value


    type DwellerInventoryPageSource = DwellerIdentifier * uint64 -> Item list
    type GetPageContext =
        abstract member dwellerInventoryPageSource : DwellerInventoryPageSource ref
    let GetPage
            (context : CommonContext) =
        (context :?> GetPageContext).dwellerInventoryPageSource.Value
            
    type DwellerInventoryPageCountSource = DwellerIdentifier -> uint64
    type GetPageCountContext =
        abstract member dwellerInventoryPageCountSource : DwellerInventoryPageCountSource ref
    let GetPageCount
            (context : CommonContext) =
        (context :?> GetPageCountContext).dwellerInventoryPageCountSource.Value



