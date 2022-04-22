cd ../docker-composes
docker-compose -p consul -f .\Infrastructure.consul.yml up -d
cd ../scripts