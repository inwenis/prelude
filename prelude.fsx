open System

// https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
let DIM                     = "\u001b[2m"
let DIM_RESET               = "\u001b[22m"
let FOREGROUND_BRIGHT_GREEN = "\u001b[92m"
let FOREGROUND_ORANGE       = "\u001b[38;5;130m"
let RESET_COLORS            = "\u001b[0m"

fsi.AddPrinter(fun (x: DateTimeOffset) ->
    let part1 = x.ToString "yyyy-MM-dd"
    let part2 = x.ToString "HH:mm:ss"
    let part3 = x.ToString ".fffffff"
    let part4 = x.ToString "zzz"
    sprintf "%s%s%s%s%s%s%s%s%s%s%s" FOREGROUND_ORANGE part1 DIM "T" DIM_RESET part2 DIM part3 DIM_RESET part4 RESET_COLORS)

fsi.AddPrinter(fun (x: DateTime) ->
    let part1 = x.ToString "yyyy-MM-dd"
    let part2 = x.ToString "HH:mm:ss"
    let part3 = x.ToString ".fffffff"
    sprintf "%s%s%s%s%s%s%s%s%s (%A)%s" FOREGROUND_ORANGE part1 DIM "T" DIM_RESET part2 DIM part3 DIM_RESET x.Kind RESET_COLORS)

fsi.AddPrinter(fun (x: DateOnly) ->
    let part1 = x.ToString "yyyy-MM-dd"
    sprintf "%s%s%s" FOREGROUND_ORANGE part1 RESET_COLORS)

// I have tested that FOREGROUND_BRIGHT_GREEN is used by default for numbers in vs code fsi.
// We would like to use the same color for our custom decimal printer.
fsi.AddPrinter(fun (x: Decimal) ->
    let formatted = x.ToString $"f{x.Scale}" + "M"
    sprintf "%s%s%s" FOREGROUND_BRIGHT_GREEN formatted RESET_COLORS)
