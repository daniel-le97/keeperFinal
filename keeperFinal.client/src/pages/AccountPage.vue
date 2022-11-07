<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-10">
        <div class="container-fluid">
          <div class="row justify-content-center">
            <div class="col-md-8">
              <ProfileDetail :profile="profile" v-if="profile" />
            </div>
          </div>
          <!--  -->
          <div class="row justify-content-evenly">
            <div class="col-12 text-center">
              <h1>vaults</h1>
            </div>
            <div class="col-3 height my-3" v-for="vault in vaults" :key="vault.id">
              <VaultCard :vault="vault" />
            </div>
          </div>
          <!--  -->
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
  </div>
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

export default {
  setup() {
    
   
    onAuthLoaded(() => {
      getAccountKeeps();
      getAccountVaults();
    })
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
    async function getAccountKeeps() {
      try {
        await profilesService.getProfileKeeps(AppState.account.id);
      } catch (error) {
        Pop.error(error);
      }
    }
    async function getAccountVaults() {
      try {
        await accountService.getAccountVaults(AppState.account.id);
      } catch (error) {
        Pop.error(error);
      }
    }
    return {
      profile: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.userVaults),
    };
  },
  components: { ProfileDetail, KeepCard, VaultCard },
};
</script>
<style scoped lang="scss">
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
