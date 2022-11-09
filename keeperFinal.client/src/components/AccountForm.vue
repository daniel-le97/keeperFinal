<template>
  <form @submit.prevent="handleAccountSubmit()">
    <!--  -->
    <div class="container">
      <div class="row justify-content-between">
        <div class="col-md-6">
          <div class="mb-3">
            <label for="recipeTitle" class="form-label">name</label>
            <input
              type="text"
              class="form-control"
              id="recipeTitle"
              v-model="editable.name"
              required
              minlength="2"
              maxlength="100"
            />
          </div>
          <div class="mb-3">
            <label for="recipeImg" class="form-label">add an picture!</label>
            <input
              type="url"
              class="form-control"
              id="recipeImg"
              v-model="editable.picture"
              required
            />
          </div>
          <div class="mb-3">
            <label for="recipeImg" class="form-label">add an cover</label>
            <input
              type="url"
              class="form-control"
              id="recipeImg"
              v-model="editable.coverImg"
              required
            />
          </div>
          <!-- <div class="mb-3">
            <label for="recipeInstruction" class="form-label"
              >Description</label
            >
            <textarea
              class="form-control"
              id="recipeInstruction"
              rows="3"
              v-model="editable.description"
              required
            ></textarea>
          </div> -->
          <div class="m-2 d-flex gap-2">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
              aria-label="Close"
            >
              close
            </button>
            <button type="submit" class="btn btn-primary">done editing?</button>
          </div>
        </div>
        <div class="col-md-4 d-flex justify-content-center p-0 rounded"
            >
          <div class="bg-dark img-card rounded p-4 d-flex justify-content-center" :class="cover? 'cover':''">
            <img
              :src="editable.picture"
              alt=""
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
          console.log(editable.value);
          // return
          const account = await accountService.editAccount(editable.value);
          // const yes = await swalsService.imagePop(editable.value.img, "undo", 'top-end', 'keep created undo?' );
          // if (yes) {
          //   await keepsService.deleteKeep(keep.id);
          //   Pop.success(`${keep.name} deleted`);
          // }
          Pop.success('account successfully edited')
          Modal.getOrCreateInstance("#accountForm").hide();
        } catch (error) {
          console.error("[]", error);
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
