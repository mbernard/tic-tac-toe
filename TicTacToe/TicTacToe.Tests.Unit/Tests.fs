namespace Tests

module Domain =
    open Swensen.Unquote
    open Expecto
    open Domain

    let Given = id
    let When handler events = handler events
    let Then expectedEvents events = events =! expectedEvents

    [<Tests>]
    let tests = 
        test "Given " {
            Given []
        }

