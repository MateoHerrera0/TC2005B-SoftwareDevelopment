import {Chart, registerables} from '/scripts/charts/chart.esm.js'
Chart.register(...registerables);


/**
 * @param {number} alpha Indicated the transparency of the color
 * @returns {string} A string of the form 'rgba(240, 50, 123, 1.0)' that represents a color
 */
 function random_color(alpha=1.0)
 {
     const r_c = () => Math.round(Math.random() * 255)
     return `rgba(${r_c()}, ${r_c()}, ${r_c()}, ${alpha}`
 }

// const ctx = document.getElementById('chart1').getContext('2d');

try
{
    const bStats = await fetch('http://localhost:5000/api/charts/:username', {
        method: 'GET'
    })

    console.log('Got a response correctly')

    if(bStats.ok)
    {
        console.log('Response is ok. Converting to Json.')

        let results = await bStats.json()

        console.log('Data converted correctly. Plotting chart 1.')

        const values = Object.values(results)

        const bStats_levels = values.map(e => e['LevelsCreated'])
        const bStats_demon = values.map(e => e['Demon'])
        const bStats_dragon = values.map(e => e['Dragon'])
        const bStats_goblin = values.map(e => e['Goblin'])
        const bStats_muddy = values.map(e => e['Muddy'])
        const bStats_zombie = values.map(e => e['Zombie'])
        const bStats_box = values.map(e => e['Box'])
        const bStats_floorSpikes = values.map(e => e['FloorSpikes'])
        const bStats_hole = values.map(e => e['Hole'])
        const bStats_ogreBoss = values.map(e => e['OgreBoss'])
        const bStats_zombieBoss = values.map(e => e['ZombieBoss'])
        const level_colors = values.map(e => random_color(0.8))


        const ctx = document.getElementById('chart1').getContext('2d');
        
        const bStatsChart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['LevelsCreated', 'Demon', 'Dragon', 'Goblin', 'Muddy', 'Zombie', 'Box', 'FloorSpikes', 'Hole', 'OgreBoss', 'ZombieBoss'],
                datasets: [
                    {
                        label: ['LevelsCreated', 'Demon', 'Dragon', 'Goblin', 'Muddy', 'Zombie', 'Box', 'FloorSpikes', 'Hole', 'OgreBoss', 'ZombieBoss'],
                        backgroundColor: level_colors,
                        data: bStats_levels, bStats_demon, bStats_dragon, bStats_goblin, bStats_muddy, bStats_zombie, bStats_box, bStats_floorSpikes, bStats_hole, bStats_ogreBoss, bStats_zombieBoss
                    }
                ]
            }
        })
    }
}

catch(error)
{
    console.log(error)
}
