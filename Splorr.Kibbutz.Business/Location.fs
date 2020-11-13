namespace Splorr.Kibbutz.Business

open Splorr.Kibbutz.Model
open System

module Location =
    let internal Default = (0,0)

    let internal ToString
            (location : Location)
            : string =
        sprintf "(%d,%d)" (location |> fst) (location |> snd)

    let internal Add
            (first : Location)
            (second : Location)
            : Location =
        ((first |> fst)+(second|>fst),
            (first |> snd)+(second |> snd))


