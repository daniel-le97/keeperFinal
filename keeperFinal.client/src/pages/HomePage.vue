<template>
  <div class="container">
    <div class="bricks mt-5">
      <div class="min elevation-5" :class="keep? '': 'skeleton-loader'" v-for="(keep,index) in keeps" :key="keep.id">
        <KeepCard :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";
import { keepsService } from "../services/KeepsService";
import Pop from "../utils/Pop";

export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      } catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
      getAllKeeps();
    });
    return {
      keeps: computed(() => AppState.keeps)
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
@media only screen and (max-width: 768px){
.bricks{
  columns: 2;
}
}
.min {
  min-height: 5vh;
}

</style>
