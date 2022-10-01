namespace DailyDos.Api

open ActivityRequesthandler
open Giraffe
open UserRequesthandler
open AuthRequestHandler
open PlanRequesthandler
open PlanRatingRequesthandler
open FreeTimeRequestHandler

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
            // PLAN
            subRoute "/plan" AuthRequestHandler.Authorize
            >=> (choose [
                GET
                >=> choose [
                    routex "(/?)" >=> PlanRequesthandler.GetAllPlans
                    routef "/%i" PlanRequesthandler.GetPlan
                    ]
                POST
                >=> choose [
                    routex "/?"
                    >=> PlanRequesthandler.PostPlan
                ]
                DELETE
                >=> choose [
                    routef "/%i" PlanRequesthandler.DeletePlan
                ]
                PATCH
                >=> choose [
                    routef "/%i" PlanRequesthandler.UpdatePlan
                ]
            ])
            // PLAN-RATING
            subRoute "/planrating" AuthRequestHandler.Authorize
            >=> (choose [
                GET
                >=> choose [
                    routef "/all/%i" PlanRatingRequesthandler.GetRatingsForActivity
                    routef "/%i" PlanRatingRequesthandler.GetRatings
                    ]
                POST
                >=> choose [
                    routex "/?"
                    >=> PlanRatingRequesthandler.PostRating
                ]
                DELETE
                >=> choose [
                    routef "/%i" PlanRatingRequesthandler.DeleteRating
                ]
                PATCH
                >=> choose [
                    routef "/%i" PlanRatingRequesthandler.UpdateRating
                ]
            ])
            // FreeTime
            subRoute "/freetime" AuthRequestHandler.Authorize
            >=> (choose [
                GET
                >=> choose [
                    routex "(/?)" >=> FreeTimeRequesthandler.GetFreeTimes
                    routef "/%i" FreeTimeRequesthandler.GetFreeTime
                    ]
                POST
                >=> choose [
                    routex "/?"
                    >=> FreeTimeRequesthandler.PostFreeTime
                ]
                DELETE
                >=> choose [
                    routef "/%i" FreeTimeRequesthandler.DeleteFreeTime
                ]
                PATCH
                >=> choose [
                    routef "/%i" FreeTimeRequesthandler.UpdateFreeTime
                ]
            ])
            // ACTIVITY
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
