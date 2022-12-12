<template>
  <div
    class="card border-0  rounded position-relative selectable"
    @click="makeActive(keep)"
    :title="keep?.name"
    >
    <img :src="keep?.img" :alt="keep.name" class="card-img img-fluid rounded img" alt="" />
    <div
    class="card-img-overlay align-items-end d-flex justify-content-between text-shadow "
    >
    <!-- <i class="mdi mdi-hospital mdi-rotate-45 position-absolute move fs-5" @click="deleteVaultKeep()"></i> -->
      <!-- <i class="mdi mdi-heart text-danger fs-1" v-if="vaulted"></i> -->

      <h5 class="card-title no-select">{{ keep?.name }}</h5>
      <img
        :src="keep.creator?.picture"
        class="person rounded-circle"
        :title="keep.creator?.name"
        :alt="keep.creator.name"
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
import { logger } from "../utils/Logger";
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
          AppState.modal = 0
          // AppState.activeKeep = keep;
          // console.log(keep);
          // if (!this.keepOwner) {
          // }
            await keepsService.getKeepById(keep);
          
          Modal.getOrCreateInstance("#detail").show();
        } catch (error) {
          Pop.error(error);
        }
      },
       async deleteVaultKeep(){
        try {
            // await vau
          } catch (error) {
            logger.error('[]',error)
            Pop.error(error)
          }
       
        }
    };
  },
};
</script>

<style lang="scss" scoped>
.card{
  transition: all 0.25s ease;
   box-shadow: 0 2px 1px -1px rgba(0, 0, 0, 0.2), 0 1px 1px 0 rgba(0, 0, 0, 0.14), 0 1px 3px 0 rgba(0, 0, 0, 0.12);
}
.card:hover{
  transition: all .25s ease;
  transform: scale(1.03);
  filter: brightness(95%);
    box-shadow: 0 2px 5px -1px rgba(0, 0, 0, 0.2), 0 5px 6px 0 rgba(0, 0, 0, 0.14), 0 1px 12px 0 rgba(0, 0, 0, 0.12);
}

// .beep::hover{
//   transform: scale(1.1);
// }

.move{
  top: -9px;
  right: -7px;
  z-index: 25;
}
.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px salmon;
  font-weight: bold;
  letter-spacing: 0.08rem;

  /* Second Color  in text-shadow is the blur */
}
</style>
