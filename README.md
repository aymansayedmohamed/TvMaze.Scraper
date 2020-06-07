# TvMaze.Scraper


## The solution components
* azure function that should run every day to sync tvmaze data at our database
* .net core API that query the synced data at our database

## steps to run the solution
* clone the repository
* Build the solution to restore the NuGet packages
* start the solution
* the azure function and the .net core api will start
* you should see the scync progress at the azure function screen  
* wait for at least one minute or till the azure function pull some data from tvmaze API and sync it with the local database
* use this URL to use the solution "http://localhost:59500/api/tvmaze?pageNumber=1&pageSize=20" 
* change the pageNumber and the pageSize with your values
