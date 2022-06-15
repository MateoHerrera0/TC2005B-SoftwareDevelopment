"use strict"

import express from 'express'
import mysql from 'mysql2/promise'
import fs from 'fs'

const app = express()
const port = 5000

app.use(express.json())

app.use('/scripts/charts', express.static('./node_modules/chart.js/dist/'))
app.use('/js', express.static('./js'))
app.use('/css', express.static('./css'))
app.use('/assets', express.static('./assets'))
app.use('/Builds/TemplateData', express.static('./Builds/TemplateData'))
app.use('/Builds/Build', express.static('./Builds/Build'))


async function connectToDB()
{
    return await mysql.createConnection({
        host:'localhost',
         user:'web2',
        //user:'AlleyCat',
         password:'1234',
        //password:'j2Qo6!fL949L',
        database:'alley_cat_db'

    })
}

app.get('/', (request,response)=>{
    fs.readFile('./html/index.html', 'utf8', (err, html)=>{
        if(err) response.status(500).send('There was an error: ' + err)
        console.log('Loading page...')
        response.send(html)
    })
})

// USER METHODS

app.get('/api/users', async (request, response)=>{
    let connection = null

    try
    {
        connection = await connectToDB()
        const [results, fields] = await connection.execute('select * from user_display')

        response.json(results)
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

app.get('/api/users/:username/:pwd', async (request, response)=>
{
    let connection = null

    try
    {
        connection = await connectToDB()

        const [results, fields] = await connection.query(`select usernameID from users where username= ? AND pwd= ?`, [request.params.username, request.params.pwd])
        if (results.length == 0) {
            response.status(400).send("No matching username or password")
        }else
        {
            response.json(results)
        }
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

app.post('/api/users', async (request, response)=>{

    let connection = null
    let symbolRegex = new RegExp('.*\\W.*')
    let emailRegex = new RegExp('\\w+(?:\\.\\w+)*@\\w+(?:\\.\\w+)*\\.[a-z]{2,4}')

    if (request.body.username.match(symbolRegex) || request.body.pwd.match(symbolRegex)) {
        // response.json({'message': "Invalid characters in username or password"})
        response.status(400).send("Invalid characters in username or password")
    } else if (!request.body.email.match(emailRegex))
    {
        response.status(400).send("Invalid email")
    } else
    {
        try
        {    
            connection = await connectToDB()
    
            const [results, fields] = await connection.query('insert into users set ?', request.body)
            
            response.json({'message': "Data inserted correctly."})
        }
        catch(error)
        {
            response.status(500)
            response.json(error)
            console.log(error)
        }
        finally
        {
            if(connection!==null) 
            {
                connection.end()
                console.log("Connection closed successfully!")
            }
        }
    }

})

app.get('/api/usersDelete/:id', async (request, response)=>{

    let connection = null
    try
    {    
        connection = await connectToDB()

        const [results, fields] = await connection.query('call deleteUser(?)', request.params.id)
        console.log(results);
        
        response.json({'message': "Called procedure correctly."})
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

//LevelMethods
app.get('/api/level', async (request, response)=>{
    let connection = null
    try
    {
        connection = await connectToDB()
        const [results, fields] = await connection.execute('select * from levels')

        response.json(results)
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

app.get('/api/level/:name', async (request, response)=>
{
    let connection = null

    try
    {
        connection = await connectToDB()

        const [results, fields] = await connection.query('select * from levels where levelName= ?', [request.params.name])
        
        response.json(results)
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

app.post('/api/level', async (request, response)=>{

    let connection = null

    try
    {    
        connection = await connectToDB()

        const [results, fields] = await connection.query('insert into levels set ?', request.body)
        
        response.json({'message': "Data inserted correctly."})
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

//Statistics Methods
app.put('/api/builderStatistics', async (request, response)=>{

    let connection = null

    try{
        connection = await connectToDB()

        const [results, fields] = await connection.query('update builderStatistics set demonEnemy = ? + demonEnemy, regularEnemy = ? + regularEnemy, dragonEnemy = ? + dragonEnemy, goblinEnemy = ? + goblinEnemy, muddyEnemy = ? + muddyEnemy, zombieEnemy = ? + zombieEnemy, boxObstacle = ? + boxObstacle, floorSpikesObstacle = ? + floorSpikesObstacle, holeObject = ? + holeObject, ogreBoss = ? + ogreBoss, zombieBoss = ? + zombieBoss, totalBuiltLevels = ? + totalBuiltLevels where usernameID = ?', [request.body['demonEnemy'], request.body['regularEnemy'], request.body['dragonEnemy'], request.body['goblinEnemy'], request.body['muddyEnemy'], request.body['zombieEnemy'], request.body['boxObstacle'], request.body['floorSpikesObstacle'], request.body['holeObject'], request.body['ogreBoss'], request.body['zombieBoss'], request.body['totalBuiltLevels'], request.body['usernameID']])
        
        response.json({'message': "Data updated correctly."})
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

app.put('/api/gameStatistics', async (request, response)=>{

    let connection = null

    try{
        connection = await connectToDB()

        const [results, fields] = await connection.query('call updateGameStatistics(?,?,?)', [request.body['usernameID'], request.body['timePlayed'], request.body['pointsGained']])
        
        response.json({'message': "Data updated correctly."})
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

app.put('/api/playerStatistics', async (request, response)=>{

    let connection = null

    try{
        connection = await connectToDB()

        const [results, fields] = await connection.query('call userActivity(?,?)', [request.body['activeBool'], request.body['usernameID']])
        
        response.json({'message': "Data updated correctly."})
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

// chart methods
app.get('/api/chart1/:username', async (request, response)=>{

    // let connection = connectToDB()
    let connection = null

    try{

        connection = await connectToDB()

        // const [results, fields] = await 
        const [results, fields] = await connection.query('select LevelsCreated, Demon, Dragon, Goblin, Muddy, Zombie, Box, FloorSpikes, Hole, OgreBoss, ZombieBoss from user_builder_stats where username=?',
        request.params.username, (error, results, fields)=>{
            if(error) console.log(error)
            console.log("Sending data correctly.")
            response.status(200)
            response.json(results)
        })

        if (results.length == 0) {
            response.status(400).send("No matching username")
        }else
        {
            response.json(results)
        }

        // connection.end()
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully!")
        }
    }
})

app.get('/api/chart2/:username', async (request, response)=>{

    // let connection = connectToDB()
    let connection = null

    try{

        connection = await connectToDB()

        // const [results, fields] = await 
        const [results, fields] = await connection.query('select HighScore, AveragePoints, TotalPoints from user_point_stats where username=?',
        request.params.username, (error, results, fields)=>{
            if(error) console.log(error)
            console.log("Sending data2 correctly.")
            response.status(200)
            response.json(results)
        })

        if (results.length == 0) {
            response.status(400).send("No matching username2")
        }else
        {
            response.json(results)
        }

        // connection.end()
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully2!")
        }
    }
})

app.get('/api/chart3/:username', async (request, response)=>{

    // let connection = connectToDB()
    let connection = null

    try{

        connection = await connectToDB()

        // const [results, fields] = await 
        const [results, fields] = await connection.query('select AverageTime, TotalTimePlayed from user_time_played  where username=?',
        request.params.username, (error, results, fields)=>{
            if(error) console.log(error)
            console.log("Sending data3 correctly.")
            response.status(200)
            response.json(results)
        })

        if (results.length == 0) {
            response.status(400).send("No matching username3")
        }else
        {
            response.json(results)
        }

        // connection.end()
    }
    catch(error)
    {
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed successfully3!")
        }
    }
})


app.listen(port, ()=>{
    console.log(`App listening at http://localhost:${port}`)
})