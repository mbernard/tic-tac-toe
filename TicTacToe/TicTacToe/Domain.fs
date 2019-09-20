namespace Domain

open Infrastructure

type Player =
    | X
    | O

type Command = Play of int

type Event = MovePlayed of int * int * Player

type GameStatus =
    | Win of Player
    | Tie
    | InProgress

module Projections =
    let project projection events =
        events |> List.fold projection.Update projection.Init

    let updateBoard (state : Player option [,]) event =
        match event with
        | MovePlayed(x, y, player) ->
            player
            |> Some
            |> Array2D.set state x y
            state

    let board : Projection<Player option [,], Event> =
        { Init = Array2D.create 3 3 None
          Update = updateBoard }
