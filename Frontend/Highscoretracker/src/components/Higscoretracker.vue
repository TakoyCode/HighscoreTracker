<script>
import { reactive } from 'vue';
import axios from 'axios';
import { onMounted } from 'vue';

export default{
    data(){
        return {
            highScoreTrackers:[]
        }
    },
    methods: {
        async fetchCount(){
            const response = await axios.get("http://localhost:5236/Highscores");
            this.highScoreTrackers = response.data;
        },
        async increment(id, num) {
            const highScoreTracker = this.highScoreTrackers.find(h => h.id == id)
            highScoreTracker.currentStreak = num;
            if(highScoreTracker.highScore < highScoreTracker.currentStreak){
                highScoreTracker.highScore = highScoreTracker.currentStreak;    
            }
            const response = await axios.put("http://localhost:5236/Highscores", highScoreTracker)
            this.fetchCount()
        },
        async AddHighScoreTrackers(){
            const highScoreTracker = {
                CurrentStreak: 0,
                HighScore: 0,
                TotalDays: 0,
            }
            const response = await axios.post("http://localhost:5236/Highscores", highScoreTracker)
            this.fetchCount()
        },
        async DeleteHighScoreTrackers(id){
            const highScoreTracker = highScoreTrackers.find(h => h.Id == id)
            const response = await axios.delete("http://localhost:5236/Highscores", highScoreTracker)
            this.fetchCount()
        }
    
    },
    mounted() {
        this.fetchCount()
    }
}
</script>

<template>
    <button @click="AddHighScoreTrackers()">jdsnfdgdfgdfgfdgvon</button><br/>
    <div class="mainmain">
        <div class="main" v-for="highScoreTracker in highScoreTrackers" :key="highScoreTracker.id">
                <div style="display: flex;">
                    <div class="content">
                    <div class="para">
                         <p>Streak: {{highScoreTracker.currentStreak}} </p>
                         <p>HighScore: {{highScoreTracker.highScore}} </p>
                         <p>Total days: {{highScoreTracker.totalDays}} </p>
                    </div>
                    <br />
                    </div>
                    <div class="content">    
                        <img :src="highScoreTracker.currentStreak > 0 ? '/img/happy.jpg' : '/img/sad.jpg'" alt="streak status">
                    </div>
                    <div class="content-button"> 
                    <button @click="increment(highScoreTracker.id, highScoreTracker.currentStreak + 1)">Done</button>
                    <br />
                    <button @click="increment(highScoreTracker.id, 0)">Not done</button>
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