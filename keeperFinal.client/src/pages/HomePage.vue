<template>
  <div class="container">
    <div class="row justify-content-center mt-5">
      <div class="col-md-10">
        <div class="container" v-if="items">
<div v-masonry="" class="d-flex  justify-content-center" transition-duration="0.3s" item-selector=".item">
  <div v-masonry-tile class="item p-2" v-for="(item, index) in items">
    <KeepCard :keep="item"/>
  </div>
</div>
          <!-- <masonry-wall :items="items" :column-width="size? 150 : 250 " :gap="16"  class="mb-5">
            <template #default="{ item, index }">
              <div >
                <KeepCard :keep="item" />
              </div>
            </template>
          </masonry-wall> -->
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
        await keepsService.getAllKeeps(AppState.offset);
      } catch (error) {
        Pop.error(error);
      }
    }
    function infiniteScroll() {
      window.onscroll = () => {
        let bottomOfWindow =
          document.documentElement.scrollTop + (window.innerHeight + 10) >=
          document.documentElement.offsetHeight;
          let scroller = AppState.scroll
          if (scroller) {
            return
          }
        if (bottomOfWindow) {
         getAllKeeps();
          console.log("hi");
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
.item{
  max-width: 250px;
}
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
  .item{
    width: 170px;
  }
}
</style>
