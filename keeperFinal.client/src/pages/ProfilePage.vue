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
          <div class="row justify-content-center">
            <div
              class="col-md-4 col-8 d-flex justify-content-between rounded-pill orange text-shadow"
            >
              <span v-if="vaults.length == 1">{{ vaults?.length }} Vault</span>
              <span v-else>{{ vaults?.length }} Vaults</span>
              <span class="bg-white one rounded"></span>
              <span>{{ items?.length }} Keeps</span>
            </div>
          </div>
          <!--  -->
          <div class="row justify-content-evenly">
            <div class="col-12 text-center">
              <h1
                class="btn fs-3"
                title="vault collapse button"
                data-bs-toggle="collapse"
                data-bs-target="#collapseExample"
                aria-expanded="false"
                aria-controls="collapseExample"
              >
                vaults <i class="mdi mdi-play mdi-rotate-90 fs-4"></i>
              </h1>
            </div>

            <div
              class="col-6 col-md-3 height my-3 collapse show"
              id="collapseExample"
              v-for="vault in vaults"
              :key="vault.id"
              
            >
              <VaultCard :vault="vault" />
            </div>
          </div>
          <!--  -->
          <div class="row">
            <div class="col-12">
              <div class="text-center"><h1>keeps</h1></div>
              <masonry-wall
                :items="items"
                :column-width="size ? 150 : 250"
                :gap="16"
              >
                <template #default="{ item, index }">
                  <div>
                    <KeepCard :keep="item" />
                  </div>
                </template>
              </masonry-wall>
            </div>
          </div>
          <!-- <div class="bricks mt-5 mb-5">
            <div
              class="min elevation-5"
              :class="keep ? '' : 'skeleton-loader'"
              v-for="(keep, index) in keeps"
              :key="keep.id"
            >
              <KeepCard :keep="keep" v-if="keep" />
            </div>
          </div> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Pop from "../utils/Pop";
import { profilesService } from "../services/ProfilesService";
import { useRoute } from "vue-router";
import { onMounted } from "vue";
import ProfileDetail from "../components/ProfileDetail.vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";

export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      getProfile();
      getProfileKeeps();
      getProfileVaults();
    });
    async function getProfile() {
      try {
        await profilesService.getProfile(route.params.id);
      } catch (error) {
        Pop.error(error);
      }
    }
    async function getProfileKeeps() {
      try {
        await profilesService.getProfileKeeps(route.params.id);
      } catch (error) {
        Pop.error(error);
      }
    }
    async function getProfileVaults() {
      try {
        await profilesService.getProfileVaults(route.params.id);
      } catch (error) {
        Pop.error(error);
      }
    }
    return {
      profile: computed(() => AppState.activeProfile),
      items: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      size: computed(() => window.innerWidth <= 768),
    };
  },
  components: { ProfileDetail, KeepCard, VaultCard },
};
</script>
<style scoped lang="scss">
.one{
  width: 2px;
}
.orange {
  background-color: rgb(250, 119, 100);
}

.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px salmon;
  font-weight: bold;
  letter-spacing: 0.08rem;

  /* Second Color  in text-shadow is the blur */
}
.bricks {
  columns: 4;
  column-fill: balance;
}
//when screen is 768px OR LESS
@media only screen and (max-width: 768px) {
  .one{
    width: 3px;
  }
  .bricks {
    columns: 2;
  }
}
.min {
  min-height: 5vh;
}
</style>
