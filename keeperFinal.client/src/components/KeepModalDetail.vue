<template>
  <div class="container-fluid" v-if="keep">
    <div class="row">
      <div class="col-md-6 p-0">
        <div class="img rounded-start"></div>
      </div>
      <div class="col-md-6 d-flex justify-content-between flex-column">
        <div class="d-flex gap-5 justify-content-center">
          <div class="d-flex gap-2">
            <i class="mdi mdi-eye mx-1"></i>
            <span>{{ keep?.views }}</span>
          </div>
          <div class="d-flex gap-2">
            <img src="../assets/img/Logo (1).png" alt="" class="no-select" />
            <span>{{ keep?.kept }}</span>
          </div>
          <div>
            <i
              class="mdi mdi-delete"
              v-if="owner"
              @click="deleteKeep(keep)"
            ></i>
          </div>
        </div>
        <div>
          <h1>{{ keep?.name }}</h1>
          <p>{{ keep?.description }}</p>
        </div>
        <div class="d-flex justify-content-between">
          <!--  inject vault stuff here TODO -->
          <div class="d-flex gap-2" v-if="!routeVault">
            <div class="dropdown open">
              <a
                class="btn btn-secondary dropdown-toggle"
                type="button"
                id="triggerId"
                data-bs-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                {{ pick ? pick.name : "vaults" }}
              </a>
              <div class="dropdown-menu" aria-labelledby="triggerId">
                <a
                  class="dropdown-item"
                  v-for="v in vaults"
                  @click="pickVault(v)"
                  >{{ v?.name }}</a
                >
              </div>
            </div>
            <button
              class="btn btn-primary"
              @click="saveKeep(keep)"
              :disabled="!pick"
            >
              save
            </button>
          </div>
          <div v-else>
            <div @click="deleteVaultKeep()">
              remove
            </div>
          </div>

          <div>
            <div>
              <img
                :src="keep?.creator.picture"
                alt=""
                height="50"
                class="rounded-circle"
                @click="getProfile(keep.creatorId)"
              />
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
    const route = useRoute()
    return {
      routeVault: computed(() => route.name == 'Vault'),
      owner: computed(() => AppState.account?.id == props.keep.creatorId),
      vaults: computed(() => AppState.userVaults),
      kept: computed(() =>{
       let kept =  AppState.vaultKeeps.find(
          (v) => v.keepId == props.keep.id && v.vaultId == AppState.vaultPick.id
        )
        return kept
    }),
      pick: computed(() => AppState.vaultPick),
      pickVault(v) {
        AppState.vaultPick = v;
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
      async deleteVaultKeep(){
        try {
                console.log(this.kept);
            // await vaultsService.deleteVaultKeep(this.kept.id)
          } catch (error) {
            Pop.error(error)
          }
      },

      async saveKeep(keep) {
        try {
          if (this.kept) {
            const yes = await swalsService.imagePop(
              this.pick.coverImg,
              "undo?",
              "center",
              `${this.pick.name} already has ${keep.name}`
            );
            console.log(yes);
            if (yes) {
              return;
            }
          }
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
</style>
