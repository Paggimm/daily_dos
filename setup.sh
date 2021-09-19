#!/bin/bash
# Setup server
(
    dotnet tool restore
    cd api
    dotnet restore
    cd ../codegen
    dotnet restore
) &

# Setup client
(
    pnpm install
) &
wait
