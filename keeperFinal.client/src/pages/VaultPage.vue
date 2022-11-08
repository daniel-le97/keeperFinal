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
                  class="card-img img-fluid height"
                  alt=""
                />
                <div
                  class="card-img-overlay d-flex flex-column justify-content-between text-shadow"
                >
                  <i class="mdi mdi-heart text-danger fs-1"></i>

                  <h1 class="card-title text-shadow text-center">
                    {{ vault?.name }}
                  </h1>
                </div>
              </div>

              <div class="dropend dots">
                <button
                  class="btn border-0"
                  type="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  <i class="mdi mdi-dots-horizontal fs-1 text-danger"></i>
                </button>
                <ul class="dropdown-menu dropdown-menu-dark">
                  <li>
                    <a class="dropdown-item" @click="editVault()">edit</a>
                  </li>
                  <li>
                    <a class="dropdown-item" @click="deleteVault(vault)"
                      >delete</a
                    >
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <!-- -->
        <div class="container">
          <div class="row mt-5">
            <div class="col-12">
              <div class="d-flex justify-content-center">
                {{ keeps?.length }}
              </div>
            </div>
          </div>
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
import { Modal } from "bootstrap";
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
    // watchEffect(() => {
    //   if (AppState.account?.id != AppState.activeVault?.creatorId) {
    //     if (AppState.activeVault?.isPrivate) {
    //       console.log("very bad user");
    //       AppState.activeVault = null;
    //       AppState.keeps = null
    //       router.push({name: 'Home'});
    //     }
    //   }
    // });

    async function getVaultById() {
      try {
        AppState.keeps = [];
        AppState.activeVault = null;
        await vaultsService.getVaultById(route.params.id);
        // console.log(AppState.activeVault);
      } catch (error) {
        console.error("[bad vault]");
        router.push({ name: "Home" });
        // Pop.error(error);
      }
    }
    async function getKeepsInVault() {
      try {
        await vaultsService.getKeepsInVault(route.params.id);
      } catch (error) {
        console.error("[bad user]", error);
        // Pop.error(error);
      }
    }
    return {
      editVault() {
        console.log("hello");
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
          console.error("[]", error);
          Pop.error(error);
        }
      },
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
  height: 400px;
  width: auto;
  object-fit: cover;
  object-position: center;
}
.min {
  min-height: 10vh;
}
</style>
