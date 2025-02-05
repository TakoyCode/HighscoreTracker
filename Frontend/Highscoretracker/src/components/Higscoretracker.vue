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

        <div v-for="highScoreTracker in highScoreTrackers" :key="highScoreTracker.ID" style="display: grid;">
                <div style="display: flex; margin-bottom: 10px;">
                    <div>
                        <span> <p>Streak: {{highScoreTracker.CurrentStreak}}</p></span>
                        <span> <p>HighScore: {{highScoreTracker.HighScore}}</p></span>
                        <span> <p>Total days: {{highScoreTracker.TotalDays}}</p></span>
                    
                    <br />
                    </div>
                    <div>    
                        <img :src="highScoreTracker.CurrentStreak > 0 ? '/img/happy.jpg' : '/img/sad.jpg'" alt="streak status">
                    </div>
                    <div style="align-items: center; align-content: center;"> 
                    <button @click="increment(highScoreTracker.ID, highScoreTracker.CurrentStreak + 1)">Done</button>
                    <br />
                    <button @click="increment(highScoreTracker.ID, 0)">Not done</button>
                    </div>
                </div>
                <br/>
        </div>
       



</template>



<style>

img {
    width: 80px;
    height: 80px;
}

p {
    width: 100px;
    height: 20px;
}

button {
    width: 100px;

}

</style>