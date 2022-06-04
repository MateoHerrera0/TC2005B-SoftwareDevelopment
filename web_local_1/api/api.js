"use strict"

import express from 'express'
import mysql from 'mysql2/promise'
import fs from 'fs'

const app = express()
const port = 3000

app.use(express.json())

app.use('/js', express.static('./js'))
app.use('/css', express.static('./css'))
app.use('/assets', express.static('./assets'))


async function connectToDB()
{
    return await mysql.createConnection({
        host:'localhost',
        user:'AlleyCat',
        password:'j2Qo6!fL949L',
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

app.get('/api/users/:username', async (request, response)=>
{
    let connection = null

    try
    {
        connection = await connectToDB()

        const [results, fields] = await connection.query('select * from user_display where username= ?', [request.params.username])
        
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

app.post('/api/users', async (request, response)=>
{

    let connection = null

    try
    {    
        connection = await connectToDB()

        // const [results, fields] = await connection.query('insert into users set username = ?, pwd = ?, email = ?', [request.body['username'], request.body['pwd'], request.body['email']])
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
})

// app.put('/api/users', async (request, response)=>{

//     let connection = null

//     try{
//         connection = await connectToDB()

//         const [results, fields] = await connection.query('update users set username = ?, pwd = ? where usernameID= ?', [request.body['username'], request.body['pwd'], request.body['usernameID']])
        
//         response.json({'message': "Data updated correctly."})
//     }
//     catch(error)
//     {
//         response.status(500)
//         response.json(error)
//         console.log(error)
//     }
//     finally
//     {
//         if(connection!==null) 
//         {
//             connection.end()
//             console.log("Connection closed successfully!")
//         }
//     }
// })

// app.delete('/api/users/:id', async (request, response)=>{

//     let connection = null

//     try
//     {
//         connection = await connectToDB()

//         const [results, fields] = await connection.query('delete from users where usernameID= ?', [request.params.id])
        
//         response.json({'message': "Data deleted correctly."})
//     }
//     catch(error)
//     {
//         response.status(500)
//         response.json(error)
//         console.log(error)
//     }
//     finally
//     {
//         if(connection!==null) 
//         {
//             connection.end()
//             console.log("Connection closed successfully!")
//         }
//     }
// })

app.get('/api/users/:username/:pwd', async (request, response)=>
{
    let connection = null

    try
    {
        connection = await connectToDB()

        const [results, fields] = await connection.query(`select usernameID from users where username= ? AND pwd= ?`, [request.params.username, request.params.pwd])
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

// LEVEL METHODS

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

app.get('/api/level/:id', async (request, response)=>
{
    let connection = null

    try
    {
        connection = await connectToDB()

        const [results, fields] = await connection.query('select * from levels where levelID= ?', [request.params.id])
        
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


app.listen(port, ()=>{
    console.log(`App listening at http://localhost:${port}`)
})