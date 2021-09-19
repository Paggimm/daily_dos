#!/bin/bash
(
    cd codegen
    dotnet run
)
dotnet fantomas api/generated/
