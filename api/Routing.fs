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
                            routex "(/?)" >=> UserRequesthandler.GetAllUsers

                            // get specific User
                            routef "/%i" UserRequesthandler.GetUserById
                        ]
                 ])
            subRoute "/activity" AuthRequestHandler.Authorize
            >=> (choose [
                     GET
                     >=> choose [
                             // get a List of a Users Activity-List
                             routex "(/?)"
                             >=> ActivityRequesthandler.GetAllActivities

                             routef "/%i" ActivityRequesthandler.GetActivity
                         ]
                     POST
                     >=> choose [
                             routex "(/?)"
                             >=> ActivityRequesthandler.PostActivity
                         ]
                     DELETE
                     >=> choose [
                             routef "/%i" ActivityRequesthandler.DeleteActivity
                         ]
                     PATCH
                     >=> choose [
                             routef "/%i" ActivityRequesthandler.PatchActivity
                         ]
                 ])
            // SERVER STATUS
            GET
            >=> choose [
                    // Ping Server Status
                    route "/ping" >=> json {| online = true |}
                    // Ping Auth Status
                    route "/authping"
                    >=> AuthRequestHandler.Authorize
                    >=> json {| online = true |}

                    ]
            // AUTH
            POST
            >=> choose [
                    // Login - Erzeugt ein JWT-Token
                    route "/login"
                    >=> AuthRequestHandler.HandlePostToken
                    // Registriere einen neuen User
                    route "/register"
                    >=> AuthRequestHandler.RegisterUser
                ]
            setStatusCode 404 >=> text "Not Found"
        ]
