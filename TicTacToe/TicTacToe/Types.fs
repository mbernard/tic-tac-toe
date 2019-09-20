namespace Infrastructure

open System

type EventSource = System.Guid

type EventProducer<'Event> = 'Event list -> 'Event list

type EventMetadata =
    { Source : EventSource
      RecordedAtUtc : DateTime }

type EventEnvelope<'Event> =
    { Metadata : EventMetadata
      Event : 'Event }

type EventHandler<'Event> = EventEnvelope<'Event> list -> Async<unit>

type EventResult<'Event> = Result<EventEnvelope<'Event> list, string>

type EventStore<'Event> =
    { Get : unit -> Async<EventResult<'Event>>
      GetStream : EventSource -> Async<EventResult<'Event>>
      Append : EventEnvelope<'Event> list -> Async<Result<unit, string>>
      OnError : IEvent<exn>
      OnEvents : IEvent<EventEnvelope<'Event> list> }

type EventListener<'Event> =
    { Subscribe : EventHandler<'Event> -> unit
      Notify : EventEnvelope<'Event> list -> unit }

type EventStorage<'Event> =
    { Get : unit -> Async<EventResult<'Event>>
      GetStream : EventSource -> Async<EventResult<'Event>>
      Append : EventEnvelope<'Event> list -> Async<unit> }

type Projection<'State, 'Event> =
    { Init : 'State
      Update : 'State -> 'Event -> 'State }

type QueryResult =
    | Handled of obj
    | NotHandled
    | QueryError of string

type QueryHandler<'Query> =
    { Handle : 'Query -> Async<QueryResult> }

type ReadModel<'Event, 'State> =
    { EventHandler : EventHandler<'Event>
      State : unit -> Async<'State> }

type CommandHandler<'Command> =
    { Handle : EventSource -> 'Command -> Async<Result<unit, string>>
      OnError : IEvent<exn> }

type Behaviour<'Command, 'Event> = 'Command -> EventProducer<'Event>

