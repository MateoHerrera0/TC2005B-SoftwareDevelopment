
import express from 'express'
import fs from 'fs'

// ---------------------------------

import mysql from 'mysql2/promise'

// ---------------------------------

// Webpage\FRONT\css\styles.css

// ---------------------------------

"use strict"

const app = express()
const port = 3000

app.use(express.json())
app.use('/css', express.static('./css'))
app.use('/assets', express.static('./assets'))
app.use('/js', express.static('./js'))


app.get('/', (req, res)=>
{
    fs.readFile('./html/index.html', 'utf8', (err, html)=>{
        if(err)
        res.status(500).send('There was an error: ' + err)
        
        res.send(html)
    })
    // fs.readFile('../BACK/html/index.html', 'utf8', (err, html)=>{
    //     if(err)
    //     res.status(500).send('There was an error: ' + err)
        
    //     res.send(html)
    // })
})

app.get('/api/users', async (req, res) =>
{
    let connection = null;

    try
    {
        connection = await mysql.createConnection(
        {
            host:'localhost', 
            user:'dev1', 
            password:'X492SwnS9m4s', 
            database: 'api_game_db'
        })
        
        console.log("Connection established!")
    
        const [rows, fields] = await connection.execute('select * from users');
        
        console.log(Object.keys(rows[0]))
    
        for (const r of rows)
        {
            console.log(Object.values(r))
        }
    
        // console.log(rows)
        // res.send(rows)
        res.json(rows)
    }
    
    catch(error)
    {
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

app.listen(port, ()=>{
    console.log(`App listening at http://localhost:${port}`)
})