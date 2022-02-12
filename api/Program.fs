namespace DailyDos.Api

open System
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.IdentityModel.Tokens
open System.Text

open DailyDos.Api
open Consts

module Program =
    // ---------------------------------
    // Error handler
    // ---------------------------------
    let errorHandler (ex: Exception) (logger: ILogger) =
        logger.LogError(ex, "An unhandled exception has occurred while executing the request.")

        clearResponse
        >=> setStatusCode 500
        >=> text ex.Message

    // ---------------------------------
    // Config and Main
    // ---------------------------------
    let configureCors (builder: CorsPolicyBuilder) =
        builder
            .WithOrigins("http://localhost:8080", "http://localhost:8081")
            .AllowAnyMethod()
            .AllowAnyHeader()
        |> ignore

    let configureApp (app: IApplicationBuilder) =
        let env = app.ApplicationServices.GetService<IWebHostEnvironment>()

        (match env.IsDevelopment() with
         | true -> app.UseDeveloperExceptionPage()
         | false ->
             app
                 .UseGiraffeErrorHandler(errorHandler)
                 .UseHttpsRedirection())
            .UseAuthentication()
            .UseCors(configureCors)
            .UseStaticFiles()
            .UseGiraffe(Routing.webApp)

    let configureServices (services: IServiceCollection) =
        services.AddCors() |> ignore
        services.AddGiraffe() |> ignore

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(fun options ->
                options.TokenValidationParameters <-
                    TokenValidationParameters(
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "jwtwebapp.net",
                        ValidAudience = "jwtwebapp.net",
                        IssuerSigningKey = SymmetricSecurityKey(Encoding.UTF8.GetBytes(Consts.secret))
                    ))
        |> ignore

    let configureLogging (builder: ILoggingBuilder) =
        builder.AddConsole().AddDebug() |> ignore

    [<EntryPoint>]
    let main args =
        let contentRoot = Directory.GetCurrentDirectory()
        let webRoot = Path.Combine(contentRoot, "WebRoot")

        Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webHostBuilder ->
                webHostBuilder
                    .UseUrls("http://0.0.0.0:8085")
                    .UseContentRoot(contentRoot)
                    .UseWebRoot(webRoot)
                    .Configure(Action<IApplicationBuilder> configureApp)
                    .ConfigureServices(configureServices)
                    .ConfigureLogging(configureLogging)
                |> ignore)
            .Build()
            .Run()

        0
