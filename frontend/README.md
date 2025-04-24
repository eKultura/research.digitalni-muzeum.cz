# To run app in Docker:

* make sure your Docker Desktop is running
* change directory to frontend folder
* run `docker build . -t "image_name"`
* check if the image was created with command `docker images`
* run `docker run -p 3000:3000 image_name`
* the app should be running on `http://localhost:3000/`