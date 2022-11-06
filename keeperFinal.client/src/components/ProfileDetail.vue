<template>
  <div class="m-3">
 <div class="card-container">
      <div class="top-card">
        <img :src="profile?.picture" alt="icon" />
      </div>
      <div class="bottom-card d-flex justify-content-center pt-5">
<h1>{{profile?.name}}</h1>
      </div>
    </div>
  </div>
 
</template>


<script>

import { computed } from '@vue/reactivity';

import { AppState } from '../AppState';
import { Profile } from '../models/Profile';


export default {
  props:{
    profile:{type: Profile, required:true}
  },
  setup(props){
    
    return {
      img: computed(() => `url(${props.profile.picture})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.card-container {
  height: 300px;
  border-radius: 12px;
  overflow: hidden;
}

.top-card {
  background-image: v-bind(img);
  height: 150px;
  display: flex;
  justify-content: center;
  align-items: flex-end;
}

.top-card img {
  width: 90px;
  height: 90px;
  border-radius: 50%;
  border: 4px solid white;
  transform: translateY(45px);
}
</style>