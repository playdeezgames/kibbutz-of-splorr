namespace Splorr.Kibbutz.Monogame

open System
open Splorr.Kibbutz.Business
open Splorr.Kibbutz.Model
open Microsoft.Xna.Framework

type internal TextCell =
    {
        character : byte
        color : Color
    }

module OutputImplementation =
    let private screenRows = 45
    let internal screenColumns = 80
    let private cellCount = screenColumns * screenRows

    let internal buffer : Map<int, TextCell> ref = ref Map.empty
    let private cursorPosition : int ref = ref 0
    let private hue : Hue ref = ref Gray

    let rec private getColorForHue
            (hue : Hue)
            : Color =
        match hue with
        | Black -> Color(0,0,0)
        | Blue -> Color(0,0,170)
        | Green -> Color(0,170,0)
        | Cyan -> Color(0,170,170)
        | Red -> Color(170,0,0)
        | Magenta -> Color(170,0,170)
        | Yellow -> Color(170,85,0)
        | Gray -> Color(170,170,170)
        | Light Black -> Color(85,85,85)
        | Light Blue -> Color(85,85,255)
        | Light Green -> Color(85,255,85)
        | Light Cyan -> Color(85,255,255)
        | Light Red -> Color(255,85,85)
        | Light Magenta -> Color(255,85,255)
        | Light Yellow -> Color(255,255,85)
        | Light Gray -> Color(255,255,255)
        | Light inner -> getColorForHue inner

    let private ScrollBuffer() : unit =
        buffer :=
            buffer.Value
            |> Map.filter
                (fun k _ -> k < screenColumns)
            |> Map.toList
            |> List.map 
                (fun (k,v) -> (k-screenColumns, v))
            |> Map.ofList

    let private AdvanceColumns
            (columnsToAdvance : int)
            : unit =
        let nextCursorPosition = cursorPosition.Value + columnsToAdvance
        if nextCursorPosition = cellCount then
            ScrollBuffer()
            cursorPosition := nextCursorPosition - screenColumns
        else
            cursorPosition := nextCursorPosition

    let private WriteCell
            (cell : TextCell)
            : unit =
        buffer := buffer.Value |> Map.add cursorPosition.Value cell
        AdvanceColumns 1

    let private WriteText
            (text : string) 
            : unit =
        text.ToCharArray()
        |> Array.iter
            (fun c -> {character = c |> byte; color = hue.Value |> getColorForHue} |> WriteCell)

    let private WriteNewLine() : unit =
        let cursorX = cursorPosition.Value % screenColumns
        let columnsToAdvance = screenColumns - cursorX
        AdvanceColumns columnsToAdvance

    let internal Backspace() : unit =
        cursorPosition:=  cursorPosition.Value - 1
        buffer:=buffer.Value |> Map.remove cursorPosition.Value

    let rec internal Write
            (message : Message)
            : unit =
        match message with
        | Text t -> 
            WriteText t
        | Line t ->
            WriteText t
            WriteNewLine()
        | Group l ->
            l
            |> List.iter Write
        | Hued (h, inner) ->
            let oldHue = hue.Value
            hue := h
            Write inner
            hue := oldHue
            
            


