cd ../docker-composes
docker-compose -f nacos.yml -f nacos.override.yml up -d
cd ../scripts