namespace Splorr.Kibbutz.Persistence

open Splorr.Kibbutz.Business

module MessagesImplementation =
    let private messages : Map<SessionIdentifier, Message list> ref = ref Map.empty

    let Get 
            (session : SessionIdentifier)
            : Message list =
        messages.Value
        |> Map.tryFind session
        |> Option.defaultValue []

    let Put
            (session: SessionIdentifier, 
                newMessages:Message list)
            : unit =
        let combinedMessages = List.append (Get session) newMessages
        messages :=
            messages.Value
            |> Map.add session combinedMessages

    let Purge
            (session: SessionIdentifier)
            : unit =
        messages := 
            messages.Value 
            |> Map.remove session



