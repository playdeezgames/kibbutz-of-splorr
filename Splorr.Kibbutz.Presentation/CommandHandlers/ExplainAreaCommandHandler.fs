namespace Splorr.Kibbutz.Presentation

open Splorr.Common
open Splorr.Kibbutz.Business
open System
open Splorr.Kibbutz.Model

module ExplainAreaCommandHandler =
    let internal Handle 
            (context : CommonContext)
            (session : SessionIdentifier)
            (location : Location)
            : SessionIdentifier option =
        raise (NotImplementedException "")

