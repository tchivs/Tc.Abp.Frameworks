cd ../docker-composes
docker-compose -p elk -f Infrastructure.elk.yml -f Infrastructure.elk.override.yml up -d
cd ../scripts