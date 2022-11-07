<template>
  <div
    class="card border-0 my-3 elevation-5 fluid rounded"
    @click="makeActive(keep)"
  >
    <img :src="keep?.img" class="card-img img-fluid" alt="" height="auto" />
    <div
      class="card-img-overlay align-items-end d-flex justify-content-between text-shadow"
    >
      <i class="mdi mdi-heart text-danger fs-1" v-if="vaulted"></i>

      <h5 class="card-title">{{ keep?.name }}</h5>
      <img
        :src="keep.creator?.picture"
        class="person rounded-circle"
        alt=""
        height="40"
        width="40"
      />
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { keepsService } from "../services/KeepsService";
import Pop from "../utils/Pop";

export default {
  props: {
    keep: { type: Keep, required: true },
  },
  setup(props) {
    return {
      keepOwner: computed(() => AppState.account.id == props.keep.creatorId),
      vaulted: computed(() => {
        let kept = AppState.vaultKeeps.find((v) => v.keepId == props.keep.id);
        // if (kept) {
        //   let keep = AppState.keeps.find((k) => k.id == kept.keepId);
        //   keep.isKept = true;
        // }
        return kept;
      }),
      async makeActive(keep) {
        try {
          AppState.activeKeep = keep;
          // console.log(keep);
          if (!this.keepOwner) {
            await keepsService.getKeepById(keep.id);
          }
          Modal.getOrCreateInstance("#detail").show();
        } catch (error) {
          Pop.error(error);
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
</style>
