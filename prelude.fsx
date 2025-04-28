open System
open System.Text.RegularExpressions

// https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
let DIM                     = "\u001b[2m"
let DIM_RESET               = "\u001b[22m"
let FOREGROUND_BRIGHT_GREEN = "\u001b[92m"
let FOREGROUND_ORANGE       = "\u001b[38;5;130m"
let RESET_COLORS            = "\u001b[0m"

// fsi.PrintWidth determines how long can a line be in the fsi window before it's wrapped.
// The default is 78. We use 33 characters (as of now) to control the colors of DateTimeOffset.
// We add 33 to fsi.PrintWidth to avoid wrapping most of the times.
// Note that the value name is included when calculating the width of the line. Hence with a long
// enough value name, the line will be wrapped.
fsi.PrintWidth <- fsi.PrintWidth + 33

let regexExtract  regex                      text = Regex.Match(text, regex).Value
let regexExtractg regex                      text = Regex.Match(text, regex).Groups.[1].Value
let regexExtracts regex                      text = Regex.Matches(text, regex) |> Seq.map (fun x -> x.Value)
let regexReplace  regex (replacement:string) text = Regex.Replace(text, regex, replacement)
let regexRemove   regex                      text = Regex.Replace(text, regex, String.Empty)

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

fsi.AddPrinter(fun (x: TimeSpan) ->
    let dimMilliseconds = regexReplace "\.\d+$" $"{DIM}$0"
    let orange s = sprintf "%s%s%s" FOREGROUND_ORANGE s RESET_COLORS

    // The BCL code for TimeSpan.ToString("c") is wild so I'll not replicate it here.
    // Instead we will format it with "c" and add our colors afterwards.
    x.ToString "c"
    |> dimMilliseconds
    |> orange)

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open System.IO

// https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/caller-information
type WorkingDirectorySetter() =
    static member SetToMe( [<CallerFilePath; Optional; DefaultParameterValue("")>] path: string) =
        Environment.CurrentDirectory <- Path.GetDirectoryName(path)
