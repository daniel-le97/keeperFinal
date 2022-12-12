<template>
  <div class="container" v-if="vault">
    <div class="row justify-content-center">
      <div class="col-md-10">
        <!--  -->
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-md-8 position-relative">
              <div class="card border-0 my-3 elevation-5 rounded">
                <img
                  :src="vault?.coverImg"
                  class="card-img img-fluid height position relative"
                  alt=""
                  :title="vault.name"                />
                <div
                  class="card-img-overlay d-flex flex-column justify-content-end text-shadow "
                >
                  <i class="mdi mdi-lock me-2 mt-2 fs-4"  title="private vault" v-if="vault.isPrivate"></i>

                  <h1 class="card-title text-shadow text-center">
                    {{ vault?.name }}
                    <div class="fs-6 mt-5"> By {{vault?.creator.name}}</div>
                  </h1>
                </div>
              </div>
              <div class="d-flex justify-content-between">
                <div class="d-flex align-items-center"><i class="mdi mdi-arrow-left fw-bold fs-1" title="back button" aria-label="back button" @click="goBack()"></i></div>
                <div class="dropend" v-if="owner">
                  <button
                    class="btn border-0 p-0"
                    type="button"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                    title="vault menu"
                  >
                    <i
                      class="mdi mdi-dots-horizontal h-25 align-top fs-1 text-danger selectable rounded"
                    ></i>
                  </button>
                  <ul class="dropdown-menu dropdown-menu-dark">
                    <li>
                      <a class="dropdown-item" title="edit vault" @click="editVault()">edit</a>
                    </li>
                    <li>
                      <a class="dropdown-item" title="delete vault" @click="deleteVault(vault)"
                        >delete</a
                      >
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- -->
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-6 rounded-pill orange">
              <div class="text-center text-shadow fs-3 d-flex gap-2 justify-content-center" v-if="items.length">
               <div>{{ items.length }}</div>
                <div>keeps</div>
              </div>
              <div class="text-center fs-3" v-else>vault is empty</div>
            </div>
          </div>
          <div class="row justify-content-center">
            <div class="col-12">
              <masonry-wall
                :items="items"
                :column-width="size ? 150 : 250"
                :gap="16"
                class="mt-5"
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
              <KeepCard :keep="keep" class="mt-5" />
            </div>
          </div> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onAuthLoaded } from "@bcwdev/auth0provider-client";
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { onMounted, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";
import { router } from "../router";
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    onMounted(() => {
      getVaultById();
      getKeepsInVault();
    });

    async function getVaultById() {
      try {
       
        AppState.activeVault = null;
        await vaultsService.getVaultById(route.params.id);
        // console.log(AppState.activeVault);
      } catch (error) {
        logger.error("[bad vault]");
        router.push({ name: "Home" });
        // Pop.error(error);
      }
    }
    async function getKeepsInVault() {
      try {
       
        await vaultsService.getKeepsInVault(route.params.id);
      } catch (error) {
        logger.error("[bad user]", error);
        // Pop.error(error);
      }
    }
    return {
      editVault() {
        AppState.modal = 1;
        AppState.vaultForm = 1;
        Modal.getOrCreateInstance("#vaultForm").show();
      },
      async deleteVault(vault) {
        try {
          // console.log(vaultId);
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          await vaultsService.deleteVault(vault.id);
          Pop.success(`${vault.name} deleted`);
          let go = router.go(-1);
          if (!go) {
            router.push({ name: "Home" });
          }
        } catch (error) {
          logger.error("[]", error);
          Pop.error(error);
        }
      },
      goBack(){
        router.go(-1)
      },
      vault: computed(() => AppState.activeVault),
      size: computed(() => window.innerWidth <= 768),
      owner: computed(
        () => AppState.account.id == AppState.activeVault.creatorId
      ),
      items: computed(() => AppState.vKeeps),
      length: computed(() => AppState.vKeeps.length < 5),
    };
  },
  components: { KeepCard },
};
</script>
<style scoped lang="scss">
.mdi-lock{
  position: absolute;
  top: 0;
  right: 0;
}
.orange {
  background-color: rgba(245, 97, 75, 0.715);
}
.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px salmon;
  font-weight: bold;
  letter-spacing: 0.08rem;

  /* Second Color  in text-shadow is the blur */
}
.dots {
  position: absolute;
  max-height: 5px;
  bottom: 20px;
  right: 10px;
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
.height {
  height: 350px;
  width: auto;
  object-fit: cover;
  object-position: center;
}
.min {
  min-height: 10vh;
}
</style>
