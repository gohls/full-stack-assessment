# Completed Screen Shot #

<img width="918" alt="Screen Shot 2024-01-21 at 7 20 58 PM" src="https://github.com/gohls/full-stack-assessment/assets/66274765/041efd8d-2071-448b-9605-53bba2c208fd">

# Full Stack Technical Assessment #

#### Quick summary ####
You will be standing up a new full stack application geared towards managing Clients and their data. The stack scaffolding has been laid out and is nicely packaged up for you in a small Docker Pod. If you are unfamiliar with Docker, that's no problem at all. We'll try to lay things out for you nice and simply down below. Acceptance Criteria (AC) for this exercise is also explained below, and should be very basic. Once you complete the basic ACs, feel free to add your own flair to your code and flex on us a bit. Once you are complete with the task, we'll sync up with you to review your code and discuss your brilliant work.

#### Keeping Track ####
We get that time spent doesn't always clearly indicate skill level or technical prowess, especially since this exercise is somwewhat open-ended. All the same, we expect the basic functionality of this excersice to take you no more than a few hours. Once you get the basics flushed out, feel free to spend as much additional time as you'd like going the extra mile and showing off. However, if you find yourself spending more than a few hours getting the basic functionality of this assessment wired up, please let us know; it's likely that you are either off the intended path, or need to beef up your skillset a bit before reapplying. We would also like you to note down things that were particularly interesting or difficult for you as you completed the exercise.

### How do I get set up? ###

Getting set up should be quite straight-forward. As this is a Docker project, please make sure you have Docker isntalled on your machine. Next, you will need to create a BitBucket account (if you don't already have one) and fork this repository. Then simply clone your forked repo and checkout a development branch on your machine (e.g. yourname-dev) with `git checkout -b <yourbranchname> origin/master` and boot things up with the commands below.

#### Installing Docker ####
Instructions for installing Docker and getting your feet wet can be found here: https://docs.docker.com/get-started/

#### Running the Project ####
The docker-compose.yml file needed to boot everything up is found in the `client-dashboard/` directory of this repository. To boot, simply navigate to this directory in your terminal and execute the following command `docker-compose -f docker-compose.yml up`

The first time you run this, this will build your Containers and boot everything up. You should see a MYSQL database, Vue frontend, and .NET backend all spin up. Your frontend should be accessible at localhost:8080 and your API at localhost:5000.

#### Some Helpful Tips ####
* After making backend code changes, make sure you run `docker-compose -f docker-compose.yml build` to rebuild and then `docker-compose -f docker-compose.yml up` again in order to see your updates. Hot reloading should be implemented on the frontend, so you won't need to do this when updating your Vue code.
* Debugging the backend is going to be dependent on the IDE that you are using. You'll need to find out how this is accomplished in your development environment on your own, but there are plenty of resources out there that will walk you through it if you just Google, "How to debug .Net Core in Docker"
* Your database and user will be created on your Container's initialization, and won't be recreated after this, no matter how many times the Containner is upped. If you need to blow away your Docker container and do a fresh installation, you can run the follwing commands
	1. `docker-compose -f docker-compose.yml down`
	2. `docker rm -f $(docker ps -a -q)`
	3. `docker volume rm $(docker volume ls -q)` (Note: this will blow away any data in your DB)
	4. `docker-compose -f docker-compose.yml up`
* There's a helpful .NET tool installed in your backend project called Swagger. This will let you view and test your API in the browser without a dedicated frontend. To access this tool, navigate to the following URL once your Containers are running: <http://localhost:5000/swagger/index.html>
* There's a decent tutorial on Dapper + .Net Core Web API here: <https://code-maze.com/using-dapper-with-asp-net-core-web-api/>

### Acceptance Criteria ###

Most of the following ACs will need to be implemented by you. These ACs serve more as a guide for what we'd like to see you accomplish rather than hard and fast rules. Don't stress too much over the minutia here. We really just want a good sampling of what you are capable of. As long as you implement each of the following bullet points in some fashion, we'll be happy to pick it apart with you.

As a base for you to work with, a basic `Clients` table has been created using the setup.sql file found in the `client-dashboard/sql-scripts/`. If you want to make edits to database schema, you'll want to do that through scripts in this file. The GetAllClients endpoint has also been created for you in order to jump start you in your API development. Please note, we are using Dapper for our ORM in this project (<https://dapper-tutorial.net/dapper>).

Reminder: Make sure you execute the commands found in the Helpful Tips section above to blow away your database after making schema changes. If you don't, your existing database will take precedence over your new changes and you might not see your updates like you expect.

#### Database ####
* We need a table to store Client records
* We need a table to store Phone numbers
* We need to associate a client with any number of phone numbers through a mapping table of some sort

#### API ####
The following endpoints are created

* GetAllClients - Returns all client records from the database
* CreateClient - Creates a new client record and stores it in database
* UpdateClient - Updates information on a client record
* ArchiveClient - Archives a client record without deleting it

#### UI ####
The following pages are created

* Client Dashboard
	* Displays all unarchived clients
	* Each client can be clicked on, which will route the user to a page in which client data can be updated
* Client Management Page
	* Client name and phone number information is editable on this page
	* A save button will persist the updates and return you back to the dashboard
	* There is also an archive button in this page that will archive the Client record and return you to the dashboard
