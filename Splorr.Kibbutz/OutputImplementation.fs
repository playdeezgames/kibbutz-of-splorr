namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open System

module OutputImplementation =
    let internal Write
            (message : Message)
            : unit =
        match message with
        | Text t ->
            Console.Write t
        | Line t ->
            Console.WriteLine t


