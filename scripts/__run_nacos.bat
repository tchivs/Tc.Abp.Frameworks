cd ../docker-composes
docker-compose -f Infrastructure.nacos.yml -f Infrastructure.nacos.override.yml up -d
cd ../scripts