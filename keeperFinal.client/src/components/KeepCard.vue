<template>
  
  <div class="card border-0 my-3 elevation-5 fluid rounded" @click="makeActive(keep)">
  <img :src="keep?.img" class="card-img img-fluid" alt="" height="auto">
  <div class="card-img-overlay align-items-end d-flex justify-content-between text-shadow">
    <h5 class="card-title">{{keep?.name}}</h5>
<img :src="keep.creator?.picture" class="person rounded-circle" alt="" height="40" width="40">

  </div>
</div>
</template>


<script>
import { Modal } from 'bootstrap';
import { AppState } from '../AppState';
import { Keep } from '../models/Keep';

export default {
  props:{
    keep: {type: Keep, required: true}
  },
  setup(){
    return {
      makeActive(keep){
        if (AppState.activeKeep != keep) {
          AppState.activeKeep = keep
          console.log(keep);
          Modal.getOrCreateInstance("#detail").show()
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

.text-shadow{
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px salmon;
  font-weight: bold;
  letter-spacing: 0.08rem
  
  /* Second Color  in text-shadow is the blur */
}
</style>