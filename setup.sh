#!/bin/bash
(
    cd api
    dotnet tool restore
    dotnet run -- Restore
)
