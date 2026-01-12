# How To Run

Make sure Docker is installed. If it isn't you can install it here https://www.docker.com/get-started/

clone the respository with `git clone https://github.com/cole853/URL-Shortening-Service.git`

With docker installed and running use `docker compose up --build` to build container images and start them.

```
git clone https://github.com/cole853/URL-Shortening-Service.git
cd URL-Shortening-Service
docker compose up --build
```

remove containers with "docker compose rm" 

http api access outside containers: localhost:5000

frontent access outside containers: localhost:8080