open System.IO

type State = {CurrentIndex: int; Totals: int[]}
let initialArraySize = 1000

let processLine (line: string) (state: State) =
    if (line.Length = 0) then
        state.CurrentIndex = state.CurrentIndex + 1 |> ignore
    else
        let value = int line
        Array.set state.Totals state.CurrentIndex (state.Totals[state.CurrentIndex] + value)
    
        
let filename = Path.Join(Directory.GetCurrentDirectory(), "../../../", "input.txt")
let state: State = {CurrentIndex = 0; Totals = Array.zeroCreate initialArraySize}

File.ReadLines(filename) |>  Seq.iter(fun (line: string) -> processLine line state)

let x = 1