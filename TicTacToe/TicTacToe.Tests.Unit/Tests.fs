module test

open Swensen.Unquote
open Expecto

[<Tests>]
let tests = 
    test "A simple test" {
        let res = Program.someFn 1
        //Expect.equal res 1 "omg"
        res =! 0
    }