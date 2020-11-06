namespace Splorr.Common

open System

module RandomUtility =
    type RandomContext =
        abstract member random : Random ref
    let internal GenerateFromRange
            (context : CommonContext)
            (minimum : float, maximum: float)
            : float =
        (context :?> RandomContext).random.Value.NextDouble() * (maximum-minimum) + minimum

    let internal SortListRandomly 
            (context : CommonContext) =
        List.sortBy (fun _ -> GenerateFromRange context (0.0, 1.0))

    let PickFromListRandomly
            (context : CommonContext) =
        SortListRandomly context >> List.head

    let GenerateFromWeightedValues
            (context : CommonContext) 
            (candidates : Map<'T, float>)
            : 'T =
        let totalWeight =
            candidates
            |> Map.toList
            |> List.map snd
            |> List.reduce (+)
        let generated = 
            GenerateFromRange context (0.0, totalWeight)
        candidates
        |> Map.fold
            (fun (result, weightLeft) item weight -> 
                match result with
                | Some _ ->
                    (result, weightLeft)
                | _ ->
                    let weightLeft = weightLeft - weight
                    if weightLeft <=0.0 then
                        (item |> Some, weightLeft)
                    else
                        (result, weightLeft)) (None, generated)
        |> fst
        |> Option.get


