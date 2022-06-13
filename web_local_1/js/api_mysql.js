import {Chart, registerables} from '/scripts/charts/chart.esm.js'
Chart.register(...registerables);

// function main()
// {

/**
 * @param {number} alpha Indicated the transparency of the color
 * @returns {string} A string of the form 'rgba(240, 50, 123, 1.0)' that represents a color
 */
function random_color(alpha=1.0)
{
    const r_c = () => Math.round(Math.random() * 255)
    return `rgba(${r_c()}, ${r_c()}, ${r_c()}, ${alpha}`
}

const ctx = document.getElementById('chart1').getContext('2d');
const ctx2 = document.getElementById('chart2').getContext('2d');
const ctx3 = document.getElementById('chart3').getContext('2d');


try
{
    console.log(document.getElementById('formSelectUser'))
    document.getElementById('formSelectUser').onsubmit = async (e) =>
    {

        e.preventDefault()

        const data = new FormData(formSelectUser)
        const dataObj = Object.fromEntries(data.entries())

        // let response = await fetch(`https://alley-cat-soul-link.herokuapp.com/api/users/${dataObj['usernameID']}`,{
        // let response = await fetch(`http://localhost:5000/api/users/${dataObj['username']}`,{
        // let response = await fetch(`http://localhost:5000/api/charts/${dataObj['username']}`,{

        const bStats = await fetch(`http://localhost:5000/api/chart1/${dataObj['username']}`, {
            method: 'GET',
            headers: {'Content-Type': 'application/json'}
        })

        
        console.log('Got a response correctly')
        
        if(bStats.ok)
        {
            console.log('Response is ok. Converting to Json.')
            
            let results = await bStats.json()
            
            console.log('Data converted correctly. Plotting chart 1.')
            
            const values = Object.values(results)
            
            const bStats_levels = values.map(e => e['LevelsCreated'])
            const level_colors = values.map(e => random_color(0.8))
            
            const bStats_demon = values.map(e => e['Demon'])
            const demon_colors = values.map(e => random_color(0.1))
            
            const bStats_dragon = values.map(e => e['Dragon'])
            const dragon_colors = values.map(e => random_color(0.2))
            
            const bStats_goblin = values.map(e => e['Goblin'])
            const goblin_colors = values.map(e => random_color(0.3))
            
            const bStats_muddy = values.map(e => e['Muddy'])
            const muddy_colors = values.map(e => random_color(0.4))
            
            const bStats_zombie = values.map(e => e['Zombie'])
            const zombie_colors = values.map(e => random_color(0.5))
            
            const bStats_box = values.map(e => e['Box'])
            const box_colors = values.map(e => random_color(0.6))
            
            const bStats_floorSpikes = values.map(e => e['FloorSpikes'])
            const floorSpikes_colors = values.map(e => random_color(0.7))
            
            const bStats_hole = values.map(e => e['Hole'])
            const hole_colors = values.map(e => random_color(0.9))
            
            const bStats_ogreBoss = values.map(e => e['OgreBoss'])
            const ogreBoss_colors = values.map(e => random_color(0.3))
            
            const bStats_zombieBoss = values.map(e => e['ZombieBoss'])
            const zombieBoss_colors = values.map(e => random_color(0.7))
            
            const ctx = document.getElementById('chart1').getContext('2d');
            
            const bStatsChart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: ['LevelsCreated', 'Demon', 'Dragon', 'Goblin', 'Muddy', 'Zombie', 'Box', 'FloorSpikes', 'Hole', 'OgreBoss', 'ZombieBoss'],
                    datasets: [
                        {
                            label: 'Builder Stats',
                            data: [bStats_levels, bStats_demon, bStats_dragon, bStats_goblin, bStats_muddy,
                                bStats_zombie, bStats_box, bStats_floorSpikes, bStats_hole, bStats_ogreBoss, bStats_zombieBoss],
                                
                                backgroundColor: [level_colors, demon_colors, dragon_colors, goblin_colors, muddy_colors, zombie_colors, box_colors,
                                    floorSpikes_colors, hole_colors, ogreBoss_colors, zombieBoss_colors],
                        }
                    ]
                },
                        
                options:
                {
                    plugins: {
                        legend: {
                            labels: {
                                color: '#FFFFFF'
                            }
                        }
                    }
                }
                        
            })
        }
                
        const gStats = await fetch(`http://localhost:5000/api/chart2/${dataObj['username']}`,{
            method: 'GET',
            headers: {'Content-Type': 'application/json'}
        })

        console.log('Got response 2 correctly')

        if(gStats.ok)
        {
            console.log('Response2 is ok. Converting to Json.')
            
            let results2 = await gStats.json()
            
            console.log('Data2 converted correctly. Plotting chart 2.')
            
            const values2 = Object.values(results2)
            
            const gStats_highScore = values2.map(e => e['HighScore'])
            const highScore_colors = values2.map(e => random_color(0.8))
            
            const gStats_averagePoints = values2.map(e => e['AveragePoints'])
            const averagePoints_colors = values2.map(e => random_color(0.1))
            
            const gStats_gamesPlayed = values2.map(e => e['GamesPlayed'])
            const gamesPlayed_colors = values2.map(e => random_color(0.2))
            
            const gStats_totalPoints = values2.map(e => e['TotalPoints'])
            const totalPoints_colors = values2.map(e => random_color(0.3))
            
            const ctx2 = document.getElementById('chart2').getContext('2d')
            
            const gStatsChart = new Chart(ctx2, {
                type: 'bar',
                data: {
                    labels: ['Game Stats'],
                    datasets: [
                        {
                            label: 'High Score',
                            data: gStats_highScore,
                            backgroundColor: highScore_colors,
                        },
                        {
                            label: 'Average Points',
                            data: gStats_averagePoints,
                            backgroundColor: averagePoints_colors,
                        },
                        {
                            label: 'Total Points',
                            data: gStats_totalPoints,
                            backgroundColor: totalPoints_colors,
                        }
                    ]
                },
                        
                options:
                {
                    plugins: {
                        legend: {
                            labels: {
                                color: '#FFFFFF'
                            },
                            y: {
                                color: '#FFFFFF'
                            }
                        }
                    }
                }
            })
        }

        const tStats = await fetch(`http://localhost:5000/api/chart3/${dataObj['username']}`,{
            method: 'GET',
            headers: {'Content-Type': 'application/json'}
        })

        console.log('Got response 3 correctly')

        if(tStats.ok)
        {
            console.log('Response3 is ok. Converting to Json.')
            
            let results3 = await tStats.json()
            
            console.log('Data3 converted correctly. Plotting chart 3.')
            
            const values3 = Object.values(results3)
            
            const tStats_averageTime = values3.map(e => e['AverageTime'])
            const averageTime_colors = values3.map(e => random_color(0.8))
            
            const tStats_totalTimePlayed = values3.map(e => e['TotalTimePlayed'])
            const totalTimePlayed_colors = values3.map(e => random_color(0.5))
            
            const ctx3 = document.getElementById('chart3').getContext('2d')
            const data3data = [tStats_averageTime, tStats_totalTimePlayed]
            
            const tStatsChart = new Chart(ctx3, {
                type: 'polarArea',
                data: {
                    labels: ['TotalTimePLayed', 'AverageTime'],
                    datasets: [
                        {
                            // label: 'TotalTimePlayed',
                            label: 'Time',
                            // label: ['Time', 'time2'],
                            data: tStats_totalTimePlayed, tStats_averageTime,
                            // data: data3data,
                            // borderColor: '#FFFFFF',
                            // backgroundColor: totalTimePlayed_colors,
                            backgroundColor: totalTimePlayed_colors, averageTime_colors
                        }
                    //     {
                    //         label: 'AverageTime',
                    //         data: tStats_averageTime,
                    //         borderColor: '#FFFFFF',
                    //         backgroundColor: averageTime_colors,

                    //     }
                    ]
                },
                        
                options:
                {
                    plugins: {
                        legend: {
                            labels: {
                                color: '#FFFFFF'
                            }
                            // y: {
                            //     color: '#FFFFFF'
                            // }
                        }
                    }
                }
                        
            });
        }

    }
            
            
}
        
        catch(error)
        {
            console.log(error)
        }
        

// console.log(document.getElementById('formSelectUser'))
// document.getElementById('formSelectUser').onsubmit = async (e) =>
// {

//     e.preventDefault()

//     const data = new FormData(formSelectUser)
//     const dataObj = Object.fromEntries(data.entries())

//     // let response = await fetch(`https://alley-cat-soul-link.herokuapp.com/api/users/${dataObj['usernameID']}`,{
//     // let response = await fetch(`http://localhost:5000/api/users/${dataObj['username']}`,{
// let response = await fetch(`http://localhost:5000/api/charts/${dataObj['username']}`,{


//         method: 'GET',
//         headers: {'Content-Type': 'application/json'},

//     })

//     if(response.ok)
//     {
//         let results = await response.json()

//         if(results.length > 0)
//         {
//             const headers = Object.keys(results[0])
//             const values = Object.values(results)

//             let table = document.createElement("table")

//             let tr = table.insertRow(-1)                  

//             for(const header of headers)
//             {
//                 let th = document.createElement("th")     
//                 th.innerHTML = header
//                 tr.appendChild(th)
//             }

//             for(const row of values)
//             {
//                 let tr = table.insertRow(-1)

//                 for(const key of Object.keys(row))
//                 {
//                     let tabCell = tr.insertCell(-1)
//                     tabCell.innerHTML = row[key]
//                 }
//             }

//             const container = document.getElementById('getResultsID')
//             container.innerHTML = ''
//             container.appendChild(table)
//         }
//         else
//         {
//             const container = document.getElementById('getResultsID')
//             container.innerHTML = 'No results to show.'
//         }
//     }
//     else{
//         getResultsID.innerHTML = response.status
//     }


// }

// document.getElementById('formInsert').onsubmit = async(e)=>
// {
//     e.preventDefault()

//     const data = new FormData(formInsert)
//     console.log(data)
//     const dataObj = Object.fromEntries(data.entries())
//     console.log(dataObj)

//     // let response = await fetch('https://alley-cat-soul-link.herokuapp.com/api/users',{
//     let response = await fetch('http://localhost:5000/api/users',{

//         method: 'POST',
//         headers: {'Content-Type': 'application/json'},
//         body: JSON.stringify(dataObj)
//     })

//     if(response.ok)
//     {
//         let results = await response.json()

//         console.log(results)
//         postResults.innerHTML = results.message
//     }
//     else{
//         postResults.innerHTML = response.status
//     }
// }

// document.getElementById('formUpdate').onsubmit = async(e)=>
// {
//     e.preventDefault()

//     const data = new FormData(formUpdate)
//     console.log(data)
//     const dataObj = Object.fromEntries(data.entries())
//     console.log(dataObj)

//     // let response = await fetch('https://alley-cat-soul-link.herokuapp.com/api/users',{
//     let response = await fetch('http://localhost:5000/api/users',{
//         method: 'PUT',
//         headers: {'Content-Type': 'application/json'},
//         body: JSON.stringify(dataObj)
//     })

//     if(response.ok)
//     {
//         let results = await response.json()

//         console.log(results)
//         putResults.innerHTML = results.message
//     }
//     else{
//         putResults.innerHTML = response.status
//     }
// }

// document.getElementById('formDelete').onsubmit = async(e)=>
// {
//     e.preventDefault()

//     const data = new FormData(formDelete)
//     const dataObj = Object.fromEntries(data.entries())

//     // let response = await fetch(`https://alley-cat-soul-link.herokuapp.com/api/users/${dataObj['usernameID']}`,{
// let response = await fetch(`http://localhost:5000/api/users${dataObj['usernameID']}`,{
//         method: 'DELETE'
//     })

//     if(response.ok)
//     {
//         let results = await response.json()

//         deleteResults.innerHTML = results.message
//     }
//     else
//     {
//         deleteResults.innerHTML = `Error!\nStatus: ${response.status} Message: ${results.message}`
//     }
// }


// import {Chart, registerables} from '/scripts/charts/chart.esm.js'
// Chart.register(...registerables);


// }



// main()

