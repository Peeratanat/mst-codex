#!/bin/sh
docker-compose up -d influxdb grafana
echo "--------------------------------------------------------------------------------------"
echo "Load testing with Grafana dashboard http://localhost:3000/d/k6/k6-load-testing-results"
echo "--------------------------------------------------------------------------------------"

docker-compose run --rm k6 run /scripts/MasterData/mst_event.js 
docker-compose run --rm k6 run /scripts/MasterData/mst_finacc.js 
docker-compose run --rm k6 run /scripts/MasterData/mst_general.js 
docker-compose run --rm k6 run /scripts/MasterData/mst_gpsim.js 
docker-compose run --rm k6 run /scripts/MasterData/mst_lg.js 
docker-compose run --rm k6 run /scripts/MasterData/mst_titledeed.js 
docker-compose run --rm k6 run /scripts/MasterData/mst_unitmeter.js

docker-compose run --rm k6 run /scripts/Project/prj_budget.js 
docker-compose run --rm k6 run /scripts/Project/prj_combineunit.js
docker-compose run --rm k6 run /scripts/Project/prj_project.js
docker-compose run --rm k6 run /scripts/Project/prj_projectinfo.js
docker-compose run --rm k6 run /scripts/Project/prj_unit.js


# Open the URL in the default browser after the test completes
if [ "$(uname)" == "Darwin" ]; then
    open http://localhost:3000/d/k6/k6-load-testing-results
elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    xdg-open http://localhost:3000/d/k6/k6-load-testing-results
elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW32_NT" ] || [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    start http://localhost:3000/d/k6/k6-load-testing-results
fi