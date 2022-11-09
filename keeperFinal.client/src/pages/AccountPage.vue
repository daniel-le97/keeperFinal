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
                      class="mdi mdi-dots-horizontal fs-1 text-danger dots selectable  rounded"
                      @click="editAccount()"
                    ></i>
                    <img :src="account?.picture" alt="icon" />
                  </div>
                  <div class="bottom-card d-flex justify-content-center ">
                  
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!--  -->

          <div class="row justify-content-evenly">
            <div class="col-12 text-center fs-1 mb-5">{{account?.name}}</div>
            <div class="col-12 text-center">
              <div class="d-flex justify-content-center gap-4 m-3" v-if="vaults">
                <div
                  @click="togglePub()"
                  :class="publicNum ? 'text-primary fw-bold' : ''"
                >
                  public <i class="mdi mdi-play mdi-rotate-90"></i>
                </div>
                <div
                  @click="togglePrivate()"
                  :class="privateNum ? 'text-primary fw-bold' : ''"
                >
                  private <i class="mdi mdi-play mdi-rotate-90"></i>
                </div>
              </div>
              <div v-else> no vaults</div>
            </div>
            <div class="col-12">
              <div class="row collapse show" id="public" v-if="vaults">
                <div class="col-3" v-for="v in vaults" :key="v?.id">
                  <VaultCard :vault="v" v-if="v" />
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
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.filterVaults),
      publicNum: computed(() => AppState.public),
      privateNum: computed(() => AppState.private),
      imgC: computed(() => `url(${AppState.account?.coverImg})`),
      editAccount() {
        AppState.modal = 1
        Modal.getOrCreateInstance("#accountForm").show();
      },
      togglePub() {
        AppState.public = !AppState.public;
        if (AppState.public == true) {
          AppState.private = false
          AppState.filterVaults = AppState.publicVaults
          return
        }
        AppState.filterVaults = AppState.userVaults
      },
      togglePrivate() {
        AppState.private = !AppState.private;
        if (AppState.private == true) {
          AppState.public = false
          AppState.filterVaults = AppState.privateVaults
          return
        }
        AppState.filterVaults = AppState.userVaults
      },
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
  height: 250px;
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
