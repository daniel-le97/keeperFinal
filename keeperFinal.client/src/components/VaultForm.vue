<template>
  <form
    @submit.prevent="vaultForm == 0 ? handleVaultSubmit() : handleVaultEdit()"
  >
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
              v-model="editable.coverImg"
              required
            />
          </div>
          <div class="mb-3">
            <label for="vaultP" class="form-label">make it private</label>
            <input
              type="checkbox"
              class="form-control"
              :class="editable.isPrivate ? 'bg-primary' : ''"
              id="vaultP"
              v-model="editable.isPrivate"
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
            <button type="submit" class="btn btn-primary">submit Vault</button>
          </div>
        </div>
        <div class="col-md-6 d-flex justify-content-center p-0">
          <div class="bg-dark img-card rounded">
            <img
              :src="editable.coverImg"
              alt=""
              class="img-fluid"
              v-if="editable.coverImg"
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
import { vaultsService } from "../services/VaultsService";

import { Modal } from "bootstrap";
import Swal from "sweetalert2";
import { swalsService } from "../services/SwalService";

export default {
  setup() {
    const editable = ref({});
    return {
      async handleVaultSubmit() {
        try {
          console.log(editable.value);
          // return
          const vault = await vaultsService.createVault(editable.value);
          // // const yes = await swalsService.imagePop(
          // //   editable.value.coverImg,
          // //   "undo",
          // //   "top-end",
          // //   "Vault created undo?"
          // // );
          // // if (yes) {
          // //   await vaultsService.deleteVault(vault.id);
          // //   Pop.success(`${vault.name} deleted`);
          // // } 
          // // else {
            Pop.success(`${vault.name} added`);
          

          Modal.getOrCreateInstance("#vaultForm").hide();
        } catch (error) {
          console.error("[]", error);
          Pop.error(error);
        }
      },
      vaultForm: computed(() => AppState.vaultForm),
      editable,
    };
  },
};
</script>

<style lang="scss" scoped>
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
