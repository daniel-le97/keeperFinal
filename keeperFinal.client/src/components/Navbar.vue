<template>
  <nav
    class="navbar navbar-expand-lg navbar-dark bg-light border-bottom border-dark px-1 px-3"
  >
    <router-link class="navbar-brand d-flex m-0" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/KeeprLogo.png" title="logo" height="35" />
      </div>
    </router-link>

    <Login  class="ms-auto"/>
    <!-- <button
      class="navbar-toggler bg-primary"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
      title="top"
    >
      <span class="navbar-toggler-icon"> </span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav ms-auto">
        <li>
        </li>
      </ul>
    
    </div> -->
  </nav>
</template>

<script>
import Login from "./Login.vue";
export default {
  setup() {
    return {};
  },
  components: { Login },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
