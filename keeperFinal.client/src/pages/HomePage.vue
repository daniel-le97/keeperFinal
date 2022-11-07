<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-10">
        <div class="container" v-if="keeps">
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
import { onMounted, onUnmounted } from "vue";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";

import { keepsService } from "../services/KeepsService";
import Pop from "../utils/Pop";
import { ref } from "vue";

export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      } catch (error) {
        Pop.error(error);
      }
    }

    function infiniteScroll() {
      window.onscroll = () => {
        let bottomOfWindow =
          document.documentElement.scrollTop + window.innerHeight ===
          document.documentElement.offsetHeight;

        if (bottomOfWindow) {
          console.log("hi");
        }
      };
    }
    onUnmounted(() => {});
    onMounted(() => {
      getAllKeeps();
      infiniteScroll();
    });
    return {
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
.min {
  min-height: 5vh;
}
</style>
