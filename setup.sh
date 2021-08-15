#!/bin/bash
# Setup server
(
    cd api
    dotnet tool restore
    dotnet run -- Restore
) &

# Setup client
(
    pnpm install
) &
wait
