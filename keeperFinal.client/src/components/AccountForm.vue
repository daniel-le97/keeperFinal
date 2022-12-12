<template>
  <form @submit.prevent="handleAccountSubmit()">
    <!--  -->
    <div class="container">
      <div class="row justify-content-between">
        <div class="col-md-6">
          <div class="mb-3">
            <label for="name" class="form-label">name</label>
            <input
              type="text"
              class="form-control"
              id="name"
              v-model="editable.name"
              required
              minlength="2"
              maxlength="100"
              title="enter a name"
            />
          </div>
          <div class="mb-3">
            <label for="pictureInput" class="form-label">add an picture!</label>
            <input
              type="url"
              class="form-control"
              id="pictureInput"
              v-model="editable.picture"
              required
              title="add a picture"
            />
          </div>
          <div class="mb-3">
            <label for="recipeImg" class="form-label">add an cover</label>
            <input
              type="url"
              class="form-control"
              id="recipeImg"
              v-model="editable.coverImg"
              title="add a cover image"
            />
          </div>
          <div class="m-2 d-flex gap-2">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
              aria-label="Close"
              title="close account form"
            >
              close
            </button>
            <button type="submit" class="btn btn-primary" title="submit account info" >done editing?</button>
          </div>
        </div>
        <div class="col-md-6 d-flex justify-content-center p-0 "
            >
          <div class="bg-dark img-card rounded-end p-4 d-flex justify-content-center align-items-center" :class="cover? 'cover':''">
            <img
              :src="editable.picture"
              class="img-fluid rounded-circle"
              v-if="editable.picture"
            />
            <div class="text-center" v-else><span>no image yet</span></div>
          </div>
        </div>
      </div>
    </div>
  </form>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { ref, watchEffect } from "vue";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";

import { Modal } from "bootstrap";
import Swal from "sweetalert2";
import { swalsService } from "../services/SwalService";
import { accountService } from "../services/AccountService";
import { logger } from "../utils/Logger";

export default {
  setup() {
    const editable = ref({});
    watchEffect(() => {
      if (AppState.account?.id) {
        editable.value = {...AppState.account}
      }
    })
    return {
      async handleAccountSubmit() {
        try {

          const yes = await Pop.confirm('Are you sure?', 'submit account info?', 'confirm', 'info')
                if (!yes) {
                  return
                }
          await accountService.editAccount(editable.value);
          Pop.success('account successfully edited')
          Modal.getOrCreateInstance("#accountForm").hide();
        } catch (error) {
          logger.error("[]", error);
          Pop.error(error);
        }
      },
      keepForm: computed(() => AppState.keepForm),
      cover: computed(() => `url(${editable.value.coverImg})`),
      editable,
    };
  },
};
</script>

<style lang="scss" scoped>
img{
  height: 50%;

  object-fit: cover;
object-position: center;
}

.cover{
  background-image: v-bind(cover);
  background-position: center;
  background-size: cover
}
.img-card {
  min-height: 20rem;
  width: 20rem;
}
.buttons {
  bottom: 20px;
  right: 30px;
  z-index: 10;
}
</style>
