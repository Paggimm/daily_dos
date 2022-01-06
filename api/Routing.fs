namespace DailyDos.Api

open Giraffe
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.AspNetCore.Http
open System.Security.Claims

open DatabaseService
open UserRequesthandler
open AuthRequestHandler

// ---------------------------------
// Web app
// ---------------------------------
module Routing =
    let webApp: HttpHandler =
        choose [
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
            GET
            >=> choose [
                    // Ping Server Status
                    route "/ping" >=> json {| online = true |}
                    // Ping Auth Status
                    route "/authping"
                    >=> AuthRequestHandler.authorize
                    >=> json {| online = true |}

                    ]
            POST
            >=> choose [
                    // Login
                    route "/token" >=> AuthRequestHandler.handlePostToken
                    route "/register" >=> AuthRequestHandler.registerUser
                ]
            setStatusCode 404 >=> text "Not Found"
        ]
