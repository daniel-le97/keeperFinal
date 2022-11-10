<template>
  <div class="container">
    <div class="row justify-content-center mt-5">
      <div class="col-md-10">
        <div class="container" v-if="items">

          <masonry-wall :items="items" :column-width="size? 150 : 250 " :gap="16" >
            <template #default="{ item, index }">
              <div >
                <KeepCard :keep="item" />
              </div>
            </template>
          </masonry-wall>
          <!-- <div class="bricks mt-5 mb-5">
            <div
              class="min"
              :class="keep ? '' : 'skeleton-loader'"
              v-for="(keep, index) in keeps"
              :key="keep.id"
            >
              <KeepCard :keep="keep" />
            </div>
          </div> -->
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

    // let timer = setTimeout(getAllKeeps, 1000)
    function infiniteScroll() {
      window.onscroll = () => {
        let bottomOfWindow =
          document.documentElement.scrollTop + window.innerHeight >=
          document.documentElement.offsetHeight;
        if (bottomOfWindow) {
          // getCurrentRecipes();
          // timer
          // getAllKeeps();
          // console.log("hi");
        }
      };
    }

    onMounted(() => {
      getAllKeeps();
      infiniteScroll();
    });
    return {
      items: computed(() => AppState.keeps),
      size: computed(() => window.innerWidth <= 768),
    };
  },
  components: { KeepCard, },
};
</script>

<style scoped lang="scss">
.hi {
  height: min-content;
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
</style>
