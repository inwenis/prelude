#load "./prelude.fsx"

open System

let a = DateTimeOffset.UtcNow
let b = DateTime.Now
let c = DateOnly.FromDateTime DateTime.Now
let d = 1.2M
let e = TimeSpan.FromHours 1
