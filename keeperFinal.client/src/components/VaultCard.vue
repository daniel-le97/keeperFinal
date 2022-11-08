<template>
  <div
    class="card border-0 my-3 elevation-5 rounded"
    @click="makeActive(vault)"
  >
    <img :src="vault?.coverImg" class="card-img img" alt="" />
    <div
      class="card-img-overlay align-items-end d-flex flex-column justify-content-between align-content-between text-shadow"
    >
      <i class="mdi mdi-lock text-white fs-6" v-if="vault?.isPrivate"></i>

      <h5 class="card-title">{{ vault?.name }}</h5>
    </div>
  </div>
</template>

<script>
import { useRouter } from "vue-router";
import { AppState } from "../AppState";
import { Vault } from "../models/Vault";
import { router } from "../router";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";

export default {
  props: {
    vault: { type: Vault, required: true },
  },
  setup() {
    const router = useRouter();
    return {
      async makeActive(vault) {
        try {
          if (!vault.isPrivate) {
            AppState.activeVault = vault;
            AppState.keeps = [];
            router.push({ name: "Vault", params: { id: vault.id } });
            document.documentElement.scrollTop = 0;
          }

          // await vaultsService.getKeepsInVault(vault.id)
        } catch (error) {
          router.push({name: 'Home'})
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px salmon;
  font-weight: bold;
  letter-spacing: 0.08rem;

  /* Second Color  in text-shadow is the blur */
}

.img {
  height: 250px;
}
</style>
