<template>
  
  <div class="container-fluid" v-if="keep">
    <div class="row">
      <div class="col-md-6 img">
        <img :src="keep?.img" alt=""  class="img-fluid "/>
      </div>
      <div class="col-md-6 d-flex justify-content-between flex-column">
        <div>
          <i class="">{{ keep?.views }}</i>
          <i class="">{{ keep?.kept }}</i>
        </div>
        <div>
          <h1>{{keep?.name}}</h1>
          <p>{{keep?.description}}</p>
        </div>
        <div class="d-flex justify-content-between">
          <!-- propably inject vault stuff here TODO -->
          <div class="dropdown open">
            <a class="btn btn-secondary dropdown-toggle" type="button" id="triggerId" data-bs-toggle="dropdown" aria-haspopup="true"
                aria-expanded="false">
                  Dropdown Anchor
                </a>
            <div class="dropdown-menu" aria-labelledby="triggerId">
              <a class="dropdown-item" href="#">Action</a>
              <a class="dropdown-item disabled" href="#">Disabled action</a>
            </div>
          </div>
          <button class="btn btn-primary">
            save
          </button>
          <div>
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { RouterLink } from 'vue-router';
import { Keep } from '../models/Keep';

export default {
    props: {
        keep: { type: Keep, required: true },
    },
    setup() {
        return {};
    },
    components: {}
};
</script>

<style lang="scss" scoped>
.img{
  min-height: 75vh;
}
</style>
