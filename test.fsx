#load "./prelude.fsx"

open System

let a = DateTimeOffset.UtcNow
let b = DateTime.Now
let c = DateOnly.FromDateTime DateTime.Now
let d = 1.2M
let e = TimeSpan.FromHours 1
let f = TimeSpan.FromMilliseconds 123
TimeSpan.FromSeconds(1231231231, microseconds=123)

[
    TimeSpan.FromHours 1
    TimeSpan.FromHours 1.2
    TimeSpan.FromDays 1
    TimeSpan.FromSeconds 1.
    TimeSpan.FromSeconds 100.
    TimeSpan.FromSeconds 1231231231.
    TimeSpan.FromMilliseconds 123
]