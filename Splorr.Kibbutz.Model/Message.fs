namespace Splorr.Kibbutz.Model

type Hue =
    | Black
    | Blue
    | Green
    | Cyan
    | Red
    | Magenta
    | Yellow
    | Gray
    | Light of Hue

type Message = 
    | Text of string
    | Line of string
    | Hued of Hue * Message
    | Group of Message list

