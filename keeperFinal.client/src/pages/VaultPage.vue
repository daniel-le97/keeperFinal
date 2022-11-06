<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-10">
        <!--  -->
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-md-8">
              <div
                class="card border-0 my-3 elevation-5 rounded"
              >
                <img :src="vault?.coverImg" class="card-img img-fluid height" alt="" />
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
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";

export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      
      getVaultById()
      getKeepsInVault();
    });
    async function getVaultById(){
      try {
          await vaultsService.getVaultById(route.params.id)
        } catch (error) {
          Pop.error(error)
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
      keeps: computed(() => AppState.keeps),
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
.height{
  height: 400px;
  width: auto;
  object-fit: cover;
  object-position: center;
}
.min {
  min-height: 10vh;
}
</style>
