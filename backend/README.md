## How to run the EntityExtractor.Web locally in Docker

1. Run `cd backend/src` to navigate into the src folder
1. Create an .env file with the following structure

    ```
    # Application port
    HTTPS_PORT=8081
    
    # PDF storage location (absolute path)
    DOCUMENT_STORAGE_PATH=<PATH-TO-YOUR-STORAGE-FOLDER>
    
    # Environment
    ENVIRONMENT=Development
    ```
1. Run `docker-compose -f docker-compose.server.yml build` to create a new image.
1. Run `docker-compose -f docker-compose.server.yml -f docker-compose.server.override.yml up` to spin up the Docker container
1. Visit https://localhost:8081/swagger/index.html to see Swagger configuration. If you wish to see openapi3 spec, go to https://localhost:8081/swagger/v1/swagger.json