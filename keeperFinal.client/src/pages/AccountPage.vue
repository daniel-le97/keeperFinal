<template>
<div class="m-3">
    <div class="card-container">
     
      <div class="top-card">
        <button @click="editAccount()">hello</button>
        <img :src="account?.picture" alt="icon" />
      </div>
      <div class="bottom-card d-flex justify-content-center pt-5">
        <h1>{{ account?.name }}</h1>
      </div>
    </div>
  </div>
  
  <!-- <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-10">
        <div class="container-fluid">
          <div class="row justify-content-center">
            <div class="col-md-8">
              <ProfileDetail :profile="profile" v-if="profile" />
            </div>
          </div>
         
          <div class="row justify-content-evenly">
            <div class="col-12 text-center">
              <h1>vaults</h1>
            </div>
            <div class="col-3 height my-3" v-for="vault in vaults" :key="vault.id">
              <VaultCard :vault="vault" />
            </div>
          </div>
       
          <div class="text-center"><h1>keeps</h1></div>
          <div class="bricks mt-5 mb-5">
            <div
              class="min elevation-5"
              :class="keep ? '' : 'skeleton-loader'"
              v-for="(keep, index) in keeps"
              :key="keep.id"
            >
              <KeepCard :keep="keep" v-if="keep" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div> -->
</template>

<script>
import Pop from "../utils/Pop";

import { useRoute } from "vue-router";
import { onMounted } from "vue";
import ProfileDetail from "../components/ProfileDetail.vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";
import { accountService } from "../services/AccountService";
import { profilesService } from "../services/ProfilesService";
import { onAuthLoaded } from "@bcwdev/auth0provider-client";
import { Modal } from "bootstrap";

export default {
  setup() {
    
   
    // onAuthLoaded(() => {
    //   getAccountKeeps();
    //   getAccountVaults();
    // })
    // onMounted(() => {
    //   getAccountKeeps();
    //   getAccountVaults();
    // })
    // async function getAccountDetails() {
    //   try {
    //     await accountService.getProfile(route.params.id);
    //   } catch (error) {
    //     Pop.error(error);
    //   }
    // }
    // async function getAccountKeeps() {
    //   try {
    //     await profilesService.getProfileKeeps(AppState.account.id);
    //   } catch (error) {
    //     Pop.error(error);
    //   }
    // }
    // async function getAccountVaults() {
    //   try {
    //     await accountService.getAccountVaults(AppState.account.id);
    //   } catch (error) {
    //     Pop.error(error);
    //   }
    // }
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.userVaults),
      imgC: computed (() => `url(${AppState.account?.coverImg})`),
       editAccount(){
        console.log('HI');
        Modal.getOrCreateInstance('#accountForm').show()
      }
    };
  },
  components: { ProfileDetail, KeepCard, VaultCard },
};
</script>
<style scoped lang="scss">
.card-container {
  height: 300px;
  border-radius: 12px;
  overflow: hidden;
}

.top-card {
  background-image: v-bind(imgC);
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
.bricks {
  columns: 4;
  column-fill: balance;
  
}
//when screen is 768px OR LESS
@media only screen and (max-width: 768px){
.bricks{
  columns: 2;
}
}
.min {
  min-height: 5vh;
}

</style>
