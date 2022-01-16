#!/bin/bash

set -e
run_cmd="dotnet WMS.NomenclatureService.dll --no-build -v d"

>&2 echo "Run NomenclatureService DB migrations"
dotnet WMS.NomenclatureService.Migrator.dll --no-build -v d -- --dryrun

dotnet WMS.NomenclatureService.Migrator.dll --no-build -v d
>&2 echo "Run NomenclatureService DB migrations complete"

>&2 echo "Run NomenclatureService: $run_cmd"
exec $run_cmd