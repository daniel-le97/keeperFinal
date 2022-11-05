<template>
  . <div class="container-fluid">
      <div class="row">
        <div class="col-md-6">
          <img :src="keep?.img" alt="">
        </div>
        <div class="col-md-6 d-flex justify-content-between">
          <div>
            <i class="">{{keep.views}}</i>
          </div>
          <div></div>
          <div></div>
        </div>
      </div>
    </div>
    
</template>


<script>
export default {
  setup(){
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>