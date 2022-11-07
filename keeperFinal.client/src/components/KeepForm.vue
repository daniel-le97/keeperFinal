<template>
  <form @submit.prevent="modalForm == 1 ? handleKeepSubmit() : handleEdit()">
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
          <!-- <div class="mb-3">
            <label for="recipeCategory" class="form-label">category</label>
            <input
              type="text"
              class="form-control"
              id="recipeCategory"
              v-model="editable.tags"
                minlength="1"
              maxlength="100"
            />
          </div> -->
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
        <div class="col-md-6 d-flex justify-content-center p-0">
          <div class="bg-dark img-card rounded">
            <img
              :src="editable.img"
              alt=""
              class="img-fluid"
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
import { swalsService} from "../services/SwalsService";
import { Modal } from "bootstrap";
import Swal from "sweetalert2";

export default {
  setup() {
    const editable = ref({});
    return {
      async handleKeepSubmit() {
        try {
         const keep =  await keepsService.createKeep(editable.value);
         const yes = await swalsService.undo(editable.value.img, undo)
          // let timerInterval;
          // const yes = await Swal.fire({
          //   title: "Auto close alert!",
          //   imageUrl: `${editable.value.img}`,
          //   position: 'top-end',
          //   imageWidth: 400,
          //   imageHeight: 200,
          //   imageAlt: "Custom image",
          //   showCloseButton: true,
          //   showCancelButton: true,
          //   reverseButtons: true,
          //   showConfirmButton: true,
          //   focusConfirm: true,
          //   confirmButtonText: '<i class="mdi mdi-keep"></i> undo',
          //   confirmButtonAriaLabel: "undo",
          //   timer: 4000,
          //   timerProgressBar: true,
          //   willClose: () => {
          //     clearInterval(timerInterval);
          //   },
          // })
          // .then((result) => {
          //   if (result.dismiss === Swal.DismissReason.timer) {
          //     console.log("I was closed by the timer");
          //   }
          // });
          // if (yes.isConfirmed) {
          //   await keepsService.deleteKeep(keep.id)
          //   Pop.success('deleted')
          // }
         

          Modal.getOrCreateInstance("#form").hide();
        } catch (error) {
          console.error("[]", error);
          Pop.error(error);
        }
      },
      modalForm: computed(() => AppState.modalForm),
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
