<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <MenuButton/>
    <!-- <button class="buttonFix box">+</button> -->
  </main>
   <footer class="bg-dark text-light">
    Made with 💖 by CodeWorks
  </footer>
  <!-- modals -->
  <KeepModal id="detail">
    <KeepModalDetail :keep="activeKeep" v-if="activeKeep"/>
  </KeepModal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepModal from './components/KeepModal.vue'
import KeepModalDetail from './components/KeepModalDetail.vue'
import MenuButton from './components/MenuButton.vue'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState),
      activeKeep: computed(() => AppState.activeKeep)
    }
  },
  components: { Navbar, KeepModal, KeepModalDetail, MenuButton }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}
.box {
  height: 50px;
  clip-path: circle(75%);
  transition: clip-path 1s;
}

.box:hover {
  clip-path: circle(25%);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}

</style>
