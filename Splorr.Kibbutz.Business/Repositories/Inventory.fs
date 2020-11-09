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

