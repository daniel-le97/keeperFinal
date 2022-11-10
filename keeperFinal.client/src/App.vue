<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <MenuButton v-if="loggedIn"/>
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
  <KeepModal id="accountForm">
    <AccountForm/>
  </KeepModal>
  <!-- modals -->
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from './AppState'
import AccountForm from './components/AccountForm.vue'
import KeepForm from './components/KeepForm.vue'
import KeepModal from './components/MainModal.vue'
import KeepModalDetail from './components/KeepModalDetail.vue'
import MenuButton from './components/MenuButton.vue'
import Navbar from './components/Navbar.vue'
import VaultForm from './components/VaultForm.vue'
import { keepsService } from './services/KeepsService'
import { useRoute } from 'vue-router'
import { vaultsService } from './services/VaultsService'

export default {
  setup() {
    // async function infiniteScroll() {
    //   window.onscroll = async()=> {
    //     let bottomOfWindow =
    //       document.documentElement.scrollTop + window.innerHeight + 10 >=
    //       document.documentElement.offsetHeight;
    //       let scroller = AppState.scroll
    //       if (scroller) {
    //         return
    //       }
    //     if (bottomOfWindow) {
    //       // getCurrentRecipes();
    //       // timer
    //        await getCurrentKeeps()
    //       // console.log("hi");
    //     }
    //   };
    // }
    // const route = useRoute()
    // async function getCurrentKeeps(){
    //      if (route.name == 'Home') {
    //        await keepsService.getAllKeeps(AppState.offset)
    //      }
      
    // }
    // onMounted(() =>{
    //   infiniteScroll()
    // })
    return {
      appState: computed(() => AppState),
      activeKeep: computed(() => AppState.activeKeep),
      loggedIn: computed(() => AppState.account?.id)
    }
  },
  components: { Navbar, KeepModal, KeepModalDetail, MenuButton, KeepForm, VaultForm, AccountForm }
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
