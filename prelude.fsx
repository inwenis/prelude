open System

// https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
let DIM                     = "\u001b[2m"
let DIM_RESET               = "\u001b[22m"
let FOREGROUND_BRIGHT_GREEN = "\u001b[92m"
let RESET_COLORS            = "\u001b[0m"

fsi.AddPrinter(fun (x: DateTimeOffset) ->
    let part1 = x.ToString "yyyy-MM-ddTHH:mm:ss"
    let part2 = x.ToString ".fffffff"
    let part3 = x.ToString "zzz"
    sprintf "%s%s%s%s%s" part1 DIM part2 DIM_RESET part3)

fsi.AddPrinter(fun (x: DateOnly) -> x.ToString "yyyy-MM-dd")

// I have tested that FOREGROUND_BRIGHT_GREEN is used by default for numbers in vs code fsi.
// We would like to use the same color for our custom decimal printer.
fsi.AddPrinter(fun (x: Decimal) ->
    let formatted = x.ToString $"f{x.Scale}" + "M"
    sprintf "%s%s%s" FOREGROUND_BRIGHT_GREEN formatted RESET_COLORS)
