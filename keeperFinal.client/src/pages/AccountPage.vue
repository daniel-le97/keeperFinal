<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-10">
        <div class="container-fluid">
          <div class="row justify-content-center">
            <div class="col-md-8">
              <div class="m-3">
                <div class="card-container">
                  <div class="top-card rounded position-relative">
                    <i
                      class="mdi mdi-dots-horizontal fs-1 text-danger dots"
                      @click="editAccount()"
                    ></i>
                    <img :src="account?.picture" alt="icon" />
                  </div>
                  <div class="bottom-card d-flex justify-content-center pt-5">
                    <h1>{{ account?.name }}</h1>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!--  -->

          <div class="row justify-content-evenly">
            <div class="col-12 text-center">
              <h1>vaults</h1>
              <div class="d-flex justify-content-between">
          <div @click="togglePub()" :class="publicNum? 'text-primary fw-bold': '' ">public <i class="mdi mdi-play mdi-rotate-90"></i></div>
            <div @click="togglePrivate()" :class="privateNum? 'text-primary fw-bold': ''">private <i class="mdi mdi-play mdi-rotate-90"></i></div>
              </div>
            </div>
            <div 
            :class="publicNum ? 'col-12': 'col-6'"
            v-if="!privateNum"
            >
    
              <div class="row collapse show" id="public" v-if="publics">
                <div :class="publicNum? 'col-3': 'col-6'" v-for="p in publics" :key="p?.id">
                  <VaultCard :vault="p" v-if="p" />
                </div>
              </div>
            </div>
            <div :class="publicNum? 'd-none': 'col-6'" v-if="!privateNum">

              <div class="row collapse show" id="private" v-if="privates">
                <div class="col-md-6" v-for="priv in privates" :key="priv?.id">
                  <VaultCard :vault="priv" />
                </div>
              </div>
            </div>
            <div class="col-12" v-else>
 
              <div class="row collapse show" id="private" v-if="privates">
                <div class="col-md-3" v-for="priv in privates" :key="priv?.id">
                  <VaultCard :vault="priv" />
                </div>
              </div>
            </div>
          </div>
          <!--  -->
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
      publics: computed(() => AppState.publicVaults),
      privates: computed(() => AppState.privateVaults),
      publicNum: computed(() => AppState.public),
      privateNum: computed(() => AppState.private),
      imgC: computed(() => `url(${AppState.account?.coverImg})`),
      editAccount() {
        Modal.getOrCreateInstance("#accountForm").show();
      },
      togglePub(){
        AppState.public = !AppState.public
        // AppState.private = !AppState.private
        console.log(AppState.public);
      },
      togglePrivate(){
        AppState.private = !AppState.private
        // AppState.public = !AppState.public
        console.log(AppState.private);
      }
    };
  },
  components: { ProfileDetail, KeepCard, VaultCard },
};
</script>
<style scoped lang="scss">
.dots {
  position: absolute;
  z-index: 10;
  bottom: -2.5rem;
  right: 0;
}
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
@media only screen and (max-width: 768px) {
  .bricks {
    columns: 2;
  }
}
.min {
  min-height: 5vh;
}
</style>
