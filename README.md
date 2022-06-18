

# TC2005B-SoftwareDevelopment

---

### Ana Paula Katsuda Zalce, Mateo Herrera Lavalle, and Gerardo Gutiérrez Paniagua

---

## How to run program:

In order to run this program it is necessary to clone the repository locally. After cloning the repo, the node js tool is needed to open the web instance. Additionally, a mySQL manager like MySQL Workbench is also necessary to run the database of the project. 

    Make sure to install npm (node package manager) when installing nodeJS 

With all the necessary tools installed, type:

    npm init

This will start creating the node instance and the json package dependencies.
In the node js json package, the dependencies are express, chart js, mysql, mysql2, and node-fetch. These will be used to connect every step of the project and make it all work together through the API. 

For the database, in the MySQL manager create a new user with the following information:

        host:'localhost',
        user:'AlleyCat',
        password:'j2Qo6!fL949L',
        database name:'alley_cat_db'

The user created should have the SELECT, INSERT, DELETE, UPDATE, and EXECUTE privileges.

Afterwards, run the ***alley_cat_db.sql*** file on the manager installing the schema of the project. And if built levels are wanted, the dummy data can be inserted into de database with one user and two built levels. 
That would be the ***FINALdummy_data_alleyCat.sql*** file. 

With the database installed and the nodejs instance created, simply change the directory on the terminal to the repository and then to the ***WEB*** folder, afterwards run the following command:

    node api/api.js

If this message pops on your console: ***App listening at http://localhost:5000***
then you can head to any browser (note that there have been issues with chrome) and enter the URL shown. This would run the designed webpage ready to be navigated and the game ready to be played in its full potential.

For further explanation on the project and the game created, the following video can provide more details: 
https://www.youtube.com/watch?v=bcNf6cKahWw

