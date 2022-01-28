#!/bin/bash

set -e
run_cmd="dotnet WMS.EmployeeService.dll --no-build -v d"

>&2 echo "Run EmployeeService DB migrations"
dotnet WMS.EmployeeService.Migrator.dll --no-build -v d -- --dryrun

dotnet WMS.EmployeeService.Migrator.dll --no-build -v d
>&2 echo "Run EmployeeService DB migrations complete"

>&2 echo "Run EmployeeService: $run_cmd"
exec $run_cmd