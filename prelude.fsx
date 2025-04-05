open System

// https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
let DIM       = "\u001b[2m"
let DIM_RESET = "\u001b[22m"

fsi.AddPrinter(fun (x: DateTimeOffset) ->
    let part1 = x.ToString "yyyy-MM-ddTHH:mm:ss"
    let part2 = x.ToString ".fffffff"
    let part3 = x.ToString "zzz"
    sprintf "%s%s%s%s%s" part1 DIM part2 DIM_RESET part3)

fsi.AddPrinter(fun (x: DateOnly) -> x.ToString "yyyy-MM-dd")
