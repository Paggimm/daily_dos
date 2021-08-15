#!/bin/bash
# Setup server
(
    cd api
    dotnet tool restore
    dotnet restore
) &

# Setup client
(
    pnpm install
) &
wait
