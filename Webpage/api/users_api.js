"use strict"

import express from 'express'
import mysql from 'mysql2/promise'
import fs from 'fs'

// ---------------------------------

// import mysql from 'mysql2/promise'

// ---------------------------------

// ---------------------------------


const app = express()
const port = process.env.PORT || 3000

app.use(express.json())

app.use('/js', express.static('./js'))
app.use('/css', express.static('./css'))
app.use('/assets', express.static('./assets'))


async function connectToDB()
{
    return await mysql.createConnection({
        host:'us-cdbr-east-05.cleardb.net',
        user:'b6d625abce8a34',
        password:'48c7415b',
        database:'heroku_29b718f30c1a210'
    })
}

app.get('/', (request,response)=>{
    fs.readFile('./html/index.html', 'utf8', (err, html)=>{
        if(err) response.status(500).send('There was an error: ' + err)
        console.log('Loading page...')
        response.send(html)
    })
})

app.get('/api/users', async (request, response)=>{
    let connection = null

    try
    {
        connection = await connectToDB()
        // const [results, fields] = await connection.execute('select * from users_display')
        const [results, fields] = await connection.execute('select * from users')

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

app.get('/api/users/:id', async (request, response)=>
{
    let connection = null

    try
    {
        connection = await connectToDB()

        // const [results, fields] = await connection.query('select * from users_display where usernameID= ?', [request.params.id])
        const [results, fields] = await connection.query('select * from users where usernameID= ?', [request.params.id])
        
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

// app.post('/api/users', async (request, response)=>
// {

//     let connection = null

//     try
//     {    
//         connection = await connectToDB()

//         // const [results, fields] = await connection.query('insert into users set username = ?, pwd = ?, email = ?', [request.body['username'], request.body['pwd'], request.body['email']])
//         const [results, fields] = await connection.query('insert into users set ?', request.body)

//         response.json({'message': "Data inserted correctly."})
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


app.listen(port, () => {
    console.log(`App listening at http://localhost:${port}`)
})