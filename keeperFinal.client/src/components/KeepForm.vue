<template>
  <form @submit.prevent="keepForm == 0 ? handleKeepSubmit() : handleKeepEdit()">
    <!--  -->
    <div class="container">
      <div class="row">
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
            <label for="recipeImg" class="form-label">add an image!</label>
            <input
              type="url"
              class="form-control"
              id="recipeImg"
              v-model="editable.img"
              required
            />
          </div>
          <div class="mb-3">
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
          </div>
          <div class="m-2 d-flex gap-2">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
              aria-label="Close"
            >
              close
            </button>
            <button type="submit" class="btn btn-primary">submit Keep</button>
          </div>
        </div>
        <div class="col-md-6 d-flex justify-content-center  p-0">
          <div class="bg-dark img-card rounded-end mb-2 mb-md-0">
            <img
              :src="editable.img"
              alt=""
              class="img-fluid h  rounded p-1"
              v-if="editable.img"
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
import { ref } from "vue";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";

import { Modal } from "bootstrap";
import Swal from "sweetalert2";
import { swalsService } from "../services/SwalService";

export default {
  setup() {
    const editable = ref({});
    return {
      async handleKeepSubmit() {
        try {
          const keep = await keepsService.createKeep(editable.value);
         
          Pop.success(`${keep.name} created`);
          editable.value = {}
          Modal.getOrCreateInstance("#keepForm").hide();
        } catch (error) {
          console.error("[]", error);
          Pop.error(error);
        }
      },
      keepForm: computed(() => AppState.keepForm),
      editable,
    };
  },
};
</script>

<style lang="scss" scoped>
img{
  height: 100%;
  object-fit: cover;
  object-position: center;
}
.img-card {
  // min-height: 20rem;
  width: 20rem;
}
.buttons {
  bottom: 20px;
  right: 30px;
  z-index: 10;
}
@media only screen and (max-width: 768px){
.img-card{
  height: 300px;
  border-bottom-left-radius: 2.5%;
  border-top-left-radius: 2.5%;
 
}
img{
  width:325px;
  object-fit: cover;
  object-position: center;
}
.mdi-lock{
  position: absolute;
  bottom: 0;
  right: 0;
}

}
</style>
