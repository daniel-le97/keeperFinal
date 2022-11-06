<template>
  <div class="card border-0 my-3 elevation-5  rounded" @click="makeActive(vault)">
  <img :src="vault?.coverImg" class="card-img img-fluid" alt="" >
  <div class="card-img-overlay align-items-end d-flex justify-content-between text-shadow">
    <i class="mdi mdi-heart text-danger fs-1"></i>
 
    <h5 class="card-title">{{vault?.name}}</h5>
  </div>
</div>
</template>


<script>

import { AppState } from '../AppState';
import { Vault } from '../models/Vault';
import { router } from '../router';
import { vaultsService } from '../services/VaultsService';
import Pop from '../utils/Pop';

export default {
   props:{
    vault: {type: Vault, required: true}
  },
  setup(){
    return {
      async makeActive(vault){
        try {
            AppState.activeVault = vault
            AppState.keeps = []
            router.push({ name: "Vault", params: { id: vault.id} });
            
        document.documentElement.scrollTop = 0;
      
            // await vaultsService.getKeepsInVault(vault.id)
            
          } catch (error) {
            Pop.error(error)
          }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>