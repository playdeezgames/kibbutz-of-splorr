namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open System
open Splorr.Common

module internal AreaCommandParser =
    let internal Parse
            (context : CommonContext)
            (session : SessionIdentifier)
            (x: string)
            (y: string)
            : Command option =
        match Int32.TryParse(x), Int32.TryParse(y) with
        | (true, locationx), (true, locationy) ->
            (locationx, locationy) 
            |> ExplainArea 
            |> Some
        | _ ->
            None

