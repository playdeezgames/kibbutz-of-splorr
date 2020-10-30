namespace Splorr.Kibbutz

open Splorr.Kibbutz.Presentation
open System

module OutputImplementation =
    let rec private getConsoleColorForHue
            (hue : Hue)
            : ConsoleColor =
        match hue with
        | Black -> ConsoleColor.Black
        | Blue -> ConsoleColor.DarkBlue
        | Green -> ConsoleColor.DarkGreen
        | Cyan -> ConsoleColor.DarkCyan
        | Red -> ConsoleColor.DarkRed
        | Magenta -> ConsoleColor.DarkMagenta
        | Yellow -> ConsoleColor.DarkYellow
        | Gray -> ConsoleColor.Gray
        | Light Black -> ConsoleColor.DarkGray
        | Light Blue -> ConsoleColor.Blue
        | Light Green -> ConsoleColor.Green
        | Light Cyan -> ConsoleColor.Cyan
        | Light Red -> ConsoleColor.Red
        | Light Magenta -> ConsoleColor.Magenta
        | Light Yellow -> ConsoleColor.Yellow
        | Light Gray -> ConsoleColor.White
        | Light inner -> getConsoleColorForHue inner

    let rec internal Write
            (message : Message)
            : unit =
        match message with
        | Text t ->
            Console.Write t
        | Line t ->
            Console.WriteLine t
        | Hued (hue, inner) ->
            let oldColor = Console.ForegroundColor
            Console.ForegroundColor <- hue |> getConsoleColorForHue
            Write inner
            Console.ForegroundColor <- oldColor


