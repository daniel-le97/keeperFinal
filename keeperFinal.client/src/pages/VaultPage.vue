<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-10">
        <!--  -->
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-md-8">
              <div class="card border-0 my-3 elevation-5 rounded">
                <img
                  :src="vault?.coverImg"
                  class="card-img img-fluid height"
                  alt=""
                />
                <div
                  class="card-img-overlay align-items-end d-flex justify-content-between text-shadow"
                >
                  <i class="mdi mdi-heart text-danger fs-1"></i>

                  <h5 class="card-title">{{ vault?.name }}</h5>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- -->
        <div class="container">
          <div class="bricks mt-5 mb-5">
            <div
              class="min elevation-5"
              :class="keep ? '' : 'skeleton-loader'"
              v-for="(keep, index) in keeps"
              :key="keep.id"
            >
              <KeepCard :keep="keep" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onAuthLoaded } from "@bcwdev/auth0provider-client";
import { computed } from "@vue/reactivity";
import { onMounted, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";
import { router } from "../router";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    onMounted(() => {
      getVaultById();
      getKeepsInVault();
    });
    // onAuthLoaded(() => {
    //      getVaultById()
    //   getKeepsInVault();
    // })
    watchEffect(() => {
      if (AppState.account?.id != AppState.activeVault?.creatorId) {
        if (AppState.activeVault?.isPrivate) {
          console.log('not mine');
          AppState.activeVault = null;
          AppState.keeps = null          
          router.push({name: 'Home'});
        }
      }
    });
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.id);
      // console.log(AppState.activeVault);
      } catch (error) {
        router.push({name: 'Home'})
        Pop.error(error);
      }
    }
    async function getKeepsInVault() {
      try {
        await vaultsService.getKeepsInVault(route.params.id);
      } catch (error) {
        console.error("[]", error);
        Pop.error(error);
      }
    }
    return {
      vault: computed(() => AppState.activeVault),
      // owner: computed(() => AppState.account.id == AppState.activeVault.creatorId && AppState.activeVault.isPrivate),
      keeps: computed(() => AppState.keeps),
      length: computed(() => AppState.keeps.length < 5),
    };
  },
  components: { KeepCard },
};
</script>
<style scoped lang="scss">
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
.height {
  height: 400px;
  width: auto;
  object-fit: cover;
  object-position: center;
}
.min {
  min-height: 10vh;
}
</style>
