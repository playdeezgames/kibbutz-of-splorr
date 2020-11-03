namespace Splorr.Kibbutz.Business

open Splorr.Kibbutz.Model
open System

module Assignment =
    let internal Default = Assignment.Rest

    let internal ToString
            (assignment : Assignment)
            : string =
        assignment.ToString()
