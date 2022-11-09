<template>
  <!-- Modal -->
  <div
    class="modal fade"
    id="exampleModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div
      class="modal-dialog  modal-fullscreen-sm modal-dialog-centered modal-scrollable"
      :class="!modalNum ? 'modal-xl': 'modal-md'"
    >
      <div class="modal-content">
        <div class="modal-body p-0">
          <slot>




          </slot>
        </div>
        <!-- <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div> -->
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";

export default {
  setup() {
    return {
      modalNum: computed(() => AppState.modal),
      img: computed(() => `url(${AppState.activeKeep?.img})`),
    };
  },
};
</script>

<style lang="scss" scoped>
.modal {
  // background-image: v-bind(img);
  //  -webkit-backdrop-filter: ;
  background: rgb(250, 119, 100);
  background: linear-gradient(
    90deg,
    rgba(250, 119, 100, 1) 0%,
    rgba(250, 252, 226, 0.6695089285714286) 100%
  );
  backdrop-filter: blur(1px) opacity(0.2);
}
</style>
