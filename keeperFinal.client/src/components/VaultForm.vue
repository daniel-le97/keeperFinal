<template>
  <form
    @submit.prevent="vaultForm == 0 ? handleVaultSubmit() : handleVaultEdit()"
  >
    <!--  -->
    <div class="container">
      <div class="row">

        <div class="col-md-6 ">
          <div class="text-center fw-bold" v-if="vaultForm == 0">vault creation</div>
          <div class="text-center fw-bold" v-else>vault edit</div>
          <div class="mb-3">
            <label for="recipeTitle" class="form-label">name</label>
            <input
              type="text"
              class="form-control"
              id="recipeTitle"
              v-model="editable.name"
              title="name"
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
              title="image input"
              v-model="editable.coverImg"
              required
            />
          </div>
          <div class="mb-3 " >
            <label for="vaultP" class="form-label">make it private</label>
            <input
              type="checkbox"
              class="form-control w-25"
            title="private checkbox"
              id="vaultP"
              width="50"
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
            <button type="submit" class="btn btn-primary" v-if="vaultForm == 0">submit Vault</button>
            <button type="submit" class="btn btn-primary" v-else>update Vault</button>
          </div>
        </div>
               <div class="col-md-6 d-flex justify-content-center  p-0 ">
                   <i class="mdi mdi-lock text-shadow p-2 fs-4" v-if="editable.isPrivate"></i>
          <div class="bg-dark img-card rounded-end mb-2 mb-md-0 " >
            <img
              :src="editable.coverImg"
              alt=""
              class="img-fluid hi rounded p-1"
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
import { ref, watchEffect } from "vue";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";

import { Modal } from "bootstrap";
import Swal from "sweetalert2";
import { swalsService } from "../services/SwalService";

export default {
  setup() {
    const editable = ref({});
    watchEffect(() => {
      if (AppState.vaultForm != 0) {
        editable.value = { ...AppState.activeVault };
      }
    });
    return {
      async handleVaultEdit() {
        try {


          const yes = await Pop.confirm('are you sure?');
          if (!yes) {
            return;
          }
          await vaultsService.editVault(editable.value);
          editable.value = {}
          Modal.getOrCreateInstance("#vaultForm").hide();
          Pop.success(`${editable.value.name} updated`)
        } catch (error) {
          console.error(error);
          Pop.error(error);
        }
      },
      async handleVaultSubmit() {
        try {
          console.log(editable.value);
          const vault = await vaultsService.createVault(editable.value);
          editable.value = {}
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
input:checked{
  background:  rgb(246, 109, 88);
}
.bg-orange{
 background:  rgb(238, 77, 52);
}
img{
  height: 100%;
  object-fit: cover;
object-position: center;
}

.text-shadow{
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px salmon;
  font-weight: bold;
  letter-spacing: 0.08rem
  
  /* Second Color  in text-shadow is the blur */
}
.mdi-lock{
  position: absolute;
  top: 0;
  right: 0;
}
.img-card {
  // min-height: 20rem;
  width: 20rem;
}
//when screen is 768px OR LESS
@media only screen and (max-width: 768px){
.img-card{
  height: 300px;
  border-bottom-left-radius: 2.5%;
  border-top-left-radius: 2.5%;
  // margin-bottom: 1rem;
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
.buttons {
  bottom: 20px;
  right: 30px;
  z-index: 10;
}
</style>
