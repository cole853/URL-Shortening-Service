# How To Run


Make sure Docker is installed. If it isn't you can install it here https://www.docker.com/get-started/


Clone the respository with `git clone https://github.com/cole853/URL-Shortening-Service.git`


With docker installed and running use `docker compose up --build` to build container images and start them.


```
git clone https://github.com/cole853/URL-Shortening-Service.git
cd URL-Shortening-Service
docker compose up --build
```


After running this, the following should be accessible:


* Backend API:          http://localhost:5000


* Frontend App:         http://localhost:8080


* API Documentation:    http://localhost:5000/swagger/index.html


# Containers Created


There are 4 containers that are created for this project:


project-db:     This is a PostgreSQL database that is used to store urls and their corresponding short codes.


backend-api:    This is the C# backend for the project that handles HTTP requests.


frontend-app:   This is the vue frontend that communicates with the backend-api to create a more user friendly experience.


unit-tests:     This container runs the unit tests for the backend-api to make sure it is working correctly.


# Endpoints


The backend-api contains 5 endpoints:


POST /api/ShortURL      This endpoint takes a url and creates a shortcode in the database. The new records id, url, shortcode, createdAt, updatedAt, and accessCount values are returned


PUT /api/ShortURL       This endpoint takes a url and the id of a record and updates the url of that record to be the new url. Then the id, url, shortcode, createdAt, updatedAt, and accessCount values are returned


GET /api/shortURL/{shortCode}   This endpoint returns the id, url, shortCode, createdAt, updatedAt, and accessCount values for the record with the shortCode that was given


DELETE /api/ShortURL/{id}   This endpoint deletes the record with the given id


GET /{shortCode}        This endpoint reroutes the user to the url corresponding to the given shortCode.


# Frontend Demonstration

Link to video: https://youtu.be/HhLvh6l5apM