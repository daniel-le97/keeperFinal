<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <MenuButton/>
    <!-- <button class="buttonFix box">+</button> -->
  </main>
  <!-- modals -->
  <KeepModal id="detail">
    <KeepModalDetail :keep="activeKeep" v-if="activeKeep"/>
  </KeepModal>
  <KeepModal id="keepForm">
    <KeepForm/>
  </KeepModal>
  <KeepModal id="vaultForm">
    <VaultForm/>
  </KeepModal>
  <!-- modals -->
</template>

<script>
import { computed, watchEffect } from 'vue'
import { AppState } from './AppState'
import KeepForm from './components/KeepForm.vue'
import KeepModal from './components/KeepModal.vue'
import KeepModalDetail from './components/KeepModalDetail.vue'
import MenuButton from './components/MenuButton.vue'
import Navbar from './components/Navbar.vue'
import VaultForm from './components/VaultForm.vue'

export default {
  setup() {
    // watchEffect(() =>{
    //   let modal = document.querySelector('modal')
    //   modal.addEventListener('hide',)
    // })
    return {
      appState: computed(() => AppState),
      activeKeep: computed(() => AppState.activeKeep)
    }
  },
  components: { Navbar, KeepModal, KeepModalDetail, MenuButton, KeepForm, VaultForm }
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


</style>
