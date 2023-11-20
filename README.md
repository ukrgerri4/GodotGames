# GodotGames
Playground project to get acquainted with Godot

# Build and push to dockerhub
1. docker-compose -f docker-compose.yml build
2. docker tag godot-games ukrgerri4/godot-games:latest
3. docker push ukrgerri4/godot-games:latest

# On server
1. docker-compose pull
2. docker-compose down
3. docker-compose up -d