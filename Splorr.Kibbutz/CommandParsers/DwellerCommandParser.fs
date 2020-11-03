namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module DwellerCommandParser =
    let internal Parse
            (context : CommonContext)
            (session : SessionIdentifier)
            (dwellerName : string)
            : Command option =
        Dweller.FindIdentifierForName context session dwellerName
        |> Option.map Command.ExplainDweller


