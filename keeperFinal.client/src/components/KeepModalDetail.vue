<template>
  <div class="container-fluid position-relative" v-if="keep">
    <div class="row">
      <div class="col-md-6 p-0">
        <div class="img rounded-start"></div>
      </div>
      <div class="col-md-6 d-flex justify-content-between flex-column">
        <div class="d-flex gap-2 justify-content-center mt-3">
          <div class="d-flex gap-2">
            <i class="mdi mdi-eye mx-1 fs-3 no-select" title="Views count"></i>
            <span class="align-self-center">{{ keep?.views }}</span>
          </div>
          <div class="d-flex gap-2 align-items-start">
            <!-- <img src="../assets/img/Logo (1).png" alt="" class="no-select" /> -->
            <i
              class="mdi mdi-alpha-k-box-outline fs-3 no-select"
              title="Amount of times this keep has been vaulted"
            ></i>
            <span class="align-self-center">{{ keep?.kept }}</span>
          </div>
          <div class="align-self-center icon me-1">
            <i
              class="mdi mdi-delete selectable fs-4 rounded"
              v-if="owner && !routeVault"
              @click="deleteKeep(keep)"
            ></i>
          </div>
        </div>
        <div>
          <h1 class="text-center">{{ keep?.name }}</h1>
          <p class="px-md-5 px-sm-2">{{ keep?.description }}</p>
        </div>
        <div class="d-flex justify-content-between mb-2">
          <!--  inject vault stuff here TODO -->
          <div class="d-flex gap-2" v-if="!routeVault && loggedIn">
            <div class="dropup open d-flex align-items-end ">
              <a
                class="btn btn-secondary dropdown-toggle btnA text-truncate"
                type="button"
                id="triggerId"
                data-bs-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
                title="vaults"
              >
                {{ pick ? pick.name : "vaults" }}
              </a>
              <div class="dropdown-menu scroll" aria-labelledby="triggerId">
                <a
                  class="dropdown-item"
                  v-for="v in vaults"
                  @click="pickVault(v,keep)"
                  >{{ v?.name }}
                  <i v-if="v?.keeper" class="mdi mdi-lock fs-6 text-dark"></i
                ></a>
              </div>
            </div>
            <!-- <button
              class="btn btn-primary"
              @click="saveKeep(keep)"
              :disabled="!pick"
            >
              save
            </button> -->
          </div>
          <div v-else class="align-self-center">
            <button class="btn bg-danger" v-if="loggedIn" @click="deleteVaultKeep()">
              remove
            </button>
          </div>

          <div>
            <div class="d-flex align-items-center gap-3 ms-2">
              <img
                :src="keep?.creator.picture"
                alt=""
                height="50"
                class="rounded-circle shadow-2"
                @click="getProfile(keep.creatorId)"
              />
              <div>{{ keep?.creator.name }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import Swal from "sweetalert2";

import { RouterLink, useRoute } from "vue-router";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { router } from "../router";
import { keepsService } from "../services/KeepsService";
import { swalsService } from "../services/SwalService";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";

export default {
  props: {
    keep: { type: Keep, required: true },
  },
  setup(props) {
    const route = useRoute();
    return {
      routeVault: computed(() => route.name == "Vault"),
      owner: computed(() => AppState.account?.id == props.keep.creatorId),
      loggedIn: computed(() => AppState.account?.id),
      vaults: computed(() => AppState.userVaults),
      kept: computed(() => {
        let kept = AppState.vaultKeeps.find(
          (v) => v.keepId == props.keep.id && v.vaultId == AppState.vaultPick.id
        );
        return kept;
      }),
      pick: computed(() => AppState.vaultPick),
     async pickVault(vault, keep) {
        try {
          AppState.vaultPick = vault
            await this.saveKeep(keep)
          } catch (error) {
            Pop.error(error)
          }
      },
      propImg: computed(() => `url(${props.keep?.img})`),
      async deleteKeep(keep) {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          await keepsService.deleteKeep(keep.id);
          Pop.success(`${keep.name} removed`);
          Modal.getOrCreateInstance("#detail").hide();
        } catch (error) {
          Pop.error(error);
        }
      },
      async deleteVaultKeep() {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          console.log(props.keep?.vaultKeepId);
          await vaultsService.deleteVaultKeep(props.keep?.vaultKeepId);
          Modal.getOrCreateInstance("#detail").hide();
          Pop.success("keep removed from vault");
        } catch (error) {
          Pop.error(error);
        }
      },

      async saveKeep(keep) {
        try {
          if (this.kept) {
            const yes = await swalsService.imagePop(
              this.pick.coverImg,
              "go to keep in vault?",
              "center",
              `${this.pick.name} already has ${keep.name}`
            );
            console.log(yes);
            if (yes) {
              router.push({
                name: "Vault",
                params: { id: this.pick.id },
              });
              Modal.getOrCreateInstance("#detail").hide();
              return;
            }
          }
          let hi = AppState.userVaults.find((v) => v.id == this.pick.id);
          hi.keeper = true;
          let vaultKeep = { vaultId: this.pick.id, keepId: keep.id };
          await keepsService.saveKeep(vaultKeep);
          Pop.success(`${keep.name} saved to  ${this.pick.name}`);
        } catch (error) {
          Pop.error(error);
        }
      },
      getProfile(id) {
        Modal.getOrCreateInstance("#detail").hide();
        router.push({ name: "Profile", params: { id: id } });

        document.documentElement.scrollTop = 0;
      },
    };
  },
  components: {},
};
</script>

<style lang="scss" scoped>
.icon{
  position: absolute;
  top: 0;
  right: 0;
}
.btnA{
  width: 150px;
}
.scroll {
  max-height: 20vh;
  overflow-y: auto;
}
.img {
  min-height: 75vh;
  // object-fit: cover;
  background-position: center;
  background-size: cover;
  background-repeat: no-repeat;
  background-image: v-bind(propImg);
}

body.swal2-shown > [aria-hidden="true"] {
  transition: 0.1s filter;
  filter: blur(4px);
  background-image: v-bind(propImg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  height: 100%;
}

//when screen is 768px OR LESS
@media only screen and (max-width: 768px) {
  .img {
    min-height: 50vh;
    border-top-right-radius: 4px;
    border-bottom-right-radius: 0px !important;
    border-bottom-left-radius: 0px !important;
  }
}
</style>
