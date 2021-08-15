namespace DailyDos.Api

open Microsoft.AspNetCore.Hosting
open Giraffe

// ---------------------------------
// Models
// ---------------------------------
type Message =
    {
        Text : string
    }

// ---------------------------------
// Views
// ---------------------------------
module Views =
    open Giraffe.ViewEngine

    let layout (content: XmlNode list) =
        html [] [
            head [] [
                title []  [ encodedText "giraffe" ]
                link [ _rel  "stylesheet"
                       _type "text/css"
                       _href "/main.css" ]
            ]
            body [] content
        ]

    let partial () =
        h1 [] [ encodedText "giraffe" ]

    let index (model : Message) =
        [
            partial()
            p [] [ encodedText model.Text ]
        ] |> layout

// ---------------------------------
// Web app
// ---------------------------------
module Routing =
    let indexHandler (name : string) =
        let greetings = sprintf "Hello %s, from Giraffe!" name
        let model     = { Text = greetings }
        let view      = Views.index model
        htmlView view

    let webApp: HttpHandler =
        choose [
            GET >=>
                choose [
                    route "/" >=> indexHandler "world"
                    routef "/hello/%s" indexHandler
                    route "/ping" >=> json {| online = true |}
                ]
            setStatusCode 404 >=> text "Not Found" ]
