namespace DailyDos.Api

open ActivityRequesthandler
open Giraffe
open UserRequesthandler
open AuthRequestHandler

// ---------------------------------
// Web app
// ---------------------------------
module Routing =
    let webApp: HttpHandler =
        choose [
            // USER
            subRoute
                "/user"
                (choose [
                    GET
                    >=> choose [
                            // get a List of all Users
                            routex "(/?)" >=> UserRequesthandler.get_all_users

                            // get specific User
                            routef "/%i" UserRequesthandler.get_user_by_id
                        ]
                 ])
            subRoute
                "/activity"
                (choose [
                    GET
                    >=> choose [
                        // get a List of a Users Activity-List
                        routex "(/?)"
                        >=> AuthRequestHandler.authorize
                        >=> ActivityRequesthandler.get_all_activities
                    ]
                    POST
                    >=> choose [
                        routex "(/?)"
                        >=> AuthRequestHandler.authorize
                        >=> ActivityRequesthandler.insert_new_activity
                    ]
                ])
            // SERVER STATUS
            GET
            >=> choose [
                    // Ping Server Status
                    route "/ping" >=> json {| online = true |}
                    // Ping Auth Status
                    route "/authping"
                    >=> AuthRequestHandler.authorize
                    >=> json {| online = true |}

                    ]
            // AUTH
            POST
            >=> choose [
                    // Login - Erzeugt ein JWT-Token
                    route "/login" >=> AuthRequestHandler.handlePostToken
                    // Registriere einen neuen User
                    route "/register" >=> AuthRequestHandler.registerUser
                ]
            setStatusCode 404 >=> text "Not Found"
        ]
