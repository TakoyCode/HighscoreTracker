<script>
import { reactive } from 'vue';
import axios from 'axios';

export default{
    data(){
    return {
        highScoreTrackers:[]
    }
    },
    methods: {
        async fetchCount(){
            const response = await axios.get("http://localhost:3000/counter");
            this.highScoreTrackers = response;
        },
        async increment(id) {
            const en = highScoreTrackers.find(h => h.Id == id)
            en.CurrentStreak++
            const response = await axios.post("http://localhost:3000/HighScore", en)
            this.fetchCount()
        },
        async 
    
    }
}
[
    {
        "CurrentStreak": 1,
        "HighScore": 0,
        "TotalDays": 0,
        "ID": 1
    }
]
</script>

<template>
<Div>{{highScoreTrackers.CurrentStreak}} - {{count > 0 ? "/assets/img/" : ":-(" }}</Div>
<button @click="increment">Done</button>
<button @click="reset">Not done</button>
</template>



<style>

</style>