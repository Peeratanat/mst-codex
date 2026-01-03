@echo off
REM Start the required Docker containers
docker-compose up -d influxdb grafana

REM Display a message with the URL to check
echo --------------------------------------------------------------------------------------
echo Load testing with Grafana dashboard http://localhost:3000/d/k6/k6-load-testing-results
echo --------------------------------------------------------------------------------------

REM Run the k6 load test
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

REM Open the URL in the default browser
start http://localhost:3000/d/k6/k6-load-testing-results
