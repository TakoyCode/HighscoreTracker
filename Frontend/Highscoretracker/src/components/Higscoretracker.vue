<script>
import { reactive } from 'vue';
import axios from 'axios';

export default{
    data(){
    return {
        highScoreTrackers:[{
            CurrentStreak: 0,
            HighScore: 0,
            TotalDays: 0,
        },
        {
            CurrentStreak: 0,
            HighScore: 0,
            TotalDays: 0,
        }]
    }
    },
    methods: {
        async fetchCount(){
            const response = await axios.get("https://localhost:7094/HighScores");
            this.highScoreTrackers = response;
        },
        async increment(id, num) {
            const highScoreTracker = highScoreTrackers.find(h => h.Id == id)
            highScoreTracker.CurrentStreak = num;
            const response = await axios.post("https://localhost:7094/HighScores", highScoreTracker)
            this.fetchCount()
        },
        async AddHighScoreTrackers(){
            const highScoreTracker = new {
                CurrentStreak: 0,
                HighScore: 0,
                TotalDays: 0,
            }
            const response = await axios.post("https://localhost:7094/HighScores", highScoreTracker)
            this.fetchCount()
        },

        async DeleteHighScoreTrackers(id){
            const highScoreTracker = highScoreTrackers.find(h => h.Id == id)
            const response = await axios.delete("https://localhost:7094/HighScores", highScoreTracker)
            this.fetchCount()
        }
    
    }
}
</script>

<template>
    <div class="mainmain">
        <div class="main" v-for="highScoreTracker in highScoreTrackers" :key="highScoreTracker.ID">
                <div style="display: flex;">
                    <div class="content">
                    <div class="para">
                         <p>Streak: {{highScoreTracker.CurrentStreak}} </p>
                         <p>HighScore: {{highScoreTracker.HighScore}} </p>
                         <p>Total days: {{highScoreTracker.TotalDays}} </p>
                    </div>
                    <br />
                    </div>
                    <div class="content">    
                        <img :src="highScoreTracker.CurrentStreak > 0 ? '/img/happy.jpg' : '/img/sad.jpg'" alt="streak status">
                    </div>
                    <div class="content-button"> 
                    <button @click="increment(highScoreTracker.ID, highScoreTracker.CurrentStreak + 1)">Done</button>
                    <br />
                    <button @click="increment(highScoreTracker.ID, 0)">Not done</button>
                    </div>
                </div>
                <br/>
        </div>
    </div>
       



</template>



<style>
.mainmain {
    width: 100vw;
}
.main {
    width: 60vw;
    height: 25vh;
    background: rgba(33, 155, 166, 0.3);
    border-radius: 10px;
    margin-bottom: 5px;
}

.content {
    display: flex;
    width: 33%;
    height: 25vh;
    margin: 0;
    text-align: center;
    justify-content: center;
    font-size: larger;
    font-family: 'Courier New', Courier, monospace;
    font-weight: bold;
}

.content:hover {
    background: rgba(33, 155, 90, 0.8);
}
.para {
    display: grid;
    text-align: right;
}
img {
   
    width: 20vh;
    height: 20vh;
}


content-button {
    width: 100%;
    height: 100%;
}

button {
    width: 200px;
    height: 25px;;
}

button:hover {
    background: rgba(33, 55, 166, 0.9);
}

</style>