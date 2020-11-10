namespace Splorr.Kibbutz.Business

open Splorr.Common
open System
open Splorr.Kibbutz.Model

module internal DwellerPageGenerator =
    type PageCounter = DwellerIdentifier -> uint64
    type PageFetcher<'T> = DwellerIdentifier * uint64 -> 'T list
    type PageRenderer<'T> = 'T -> Message

    let private UnknownDwellerMessage =
        [
            Line "Unknown dweller."
        ]
        |> Group

    let private NoPageForDwellerMessage (pageType: string)=
        [
            Line (sprintf "No %s for dweller." pageType)
        ]
        |> Group

    let private PageForKnownDweller
            (context : CommonContext)
            (pageType : string)
            (pageCounter : PageCounter)
            (pageFetcher : PageFetcher<'T>)
            (pageRenderer : PageRenderer<'T>)
            (identifier : DwellerIdentifier)
            (page : uint64)
            : Message =
        let page = if page=0UL then 1UL else page
        let pages = pageCounter identifier
        let messages =
            pageFetcher (identifier, page)
            |> List.map pageRenderer
        match messages with
        | [] ->
            NoPageForDwellerMessage pageType
        | history ->
            Group
                [
                    (page, pages) ||> sprintf "Page %u of %u:" |> Line
                    Group messages
                ]

    let internal Generate
            (context : CommonContext)
            (session : SessionIdentifier)
            (pageType : string)
            (pageCounter : PageCounter)
            (pageFetcher : PageFetcher<'T>)
            (pageRenderer : PageRenderer<'T>)
            (identifier : DwellerIdentifier)
            (page : uint64)
            : Message =
        if DwellerSession.ExistsForSession context session identifier then
            PageForKnownDweller 
                context 
                pageType
                pageCounter
                pageFetcher
                pageRenderer
                identifier 
                page
        else
            UnknownDwellerMessage


