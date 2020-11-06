namespace Splorr.Kibbutz.Business

open Splorr.Kibbutz.Model
open System

module Location =
    let internal Default = (0,0)

    let internal ToString
            (location : Location)
            : string =
        match location with
        | (0,0) -> "c"
        | (x,y) when x = 0 && y > 0 -> y |> sprintf "s%d"
        | (x,y) when x = 0 && y < 0 -> (-y) |> sprintf "n%d"
        | (x,y) when y = 0 && x > 0 -> x |> sprintf "e%d"
        | (x,y) when y = 0 && x < 0 -> (-x) |> sprintf "w%d"
        | (x,y) when y > 0 && x > 0 -> (y,x) ||> sprintf "s%de%d"
        | (x,y) when y > 0 && x < 0 -> (y,-x) ||> sprintf "s%dw%d"
        | (x,y) when y < 0 && x > 0 -> (-y,x) ||> sprintf "n%de%d"
        | (x,y) when y < 0 && x < 0 -> (-y,-x) ||> sprintf "n%dw%d"
        | _ ->
            raise (NotImplementedException "Apparently, you can't think of everything!")

    let internal Add
            (first : Location)
            (second : Location)
            : Location =
        ((first |> fst)+(second|>fst),
            (first |> snd)+(second |> snd))


