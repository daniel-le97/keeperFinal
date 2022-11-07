<template>
  <div class="fix">
<nav>
	<div class="toggle">
		<input type="checkbox">
    <span class="btn"></span>
    <ul>
			<li><a href="#/" class="text-center"><i class="mdi mdi-home fs-1"></i></a></li>
			<li><a href="#/account" class="text-center"><i class="mdi mdi-account fs-1"></i></a></li>
			<li><a  class="text-center"><i class="mdi mdi-plus fs-1" @click="getKeepForm()"></i></a></li>
			<li><a  class="text-center"><i class="mdi mdi-heart fs-1" @click="$emit('getkeepForm')"></i></a></li>
			<li><a href=""><i class="fa fa-facebook"></i></a></li>

		</ul>
	</div>
</nav>
  </div>

</template>

<script>
import { Modal } from 'bootstrap';
import { AppState } from '../AppState';

export default {
  setup() {
    return {
      getKeepForm(){
        AppState.modalForm = 1
        Modal.getOrCreateInstance('#form').show()
      }
    };
  },
};
</script>

<style lang="scss" scoped>
.fix{
  position: fixed;
  bottom: 90px;
  right: 100px;
}
li::marker{
  content:'';
}


nav {
  position: absolute;
  width: 80px;
  height: 80px;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  
 

  .toggle {
    height: 100%;
    width: 100%;
  }

  input[type="checkbox"] {
    opacity: 0;
    position: absolute;
    cursor: pointer;
    z-index: 1;
    height: 100%;
    width: 100%;
    left: 0;
    top: 0;
    z-index: 10;

    &:checked {
      ~ .btn {
        &:after,
        &:before {
          transform: rotate(225deg);
        }
      }

      ~ ul li {
        @for $i from 1 through 6 {
          &:nth-child(#{$i}) {
            transform: rotate($i * 60deg) translate(6em) rotate(-$i * 60deg);
          }
        }
      }
    }

    &:hover {
      ~ .btn {
        background-color:  rgb(238, 77, 52);
      }
    }
  }
  .btn {
    display: block;
    width: 100%;
    height: 100%;
    position: relative;
    background-color: rgb(250,119,100);
    border-radius: 50%;
    cursor: pointer;
    transition: transform 1.25s cubic-bezier(0.8, 0, 0.2, 1),
      background-color 0.25s;
    z-index: 9;
    box-shadow: 0 5px 25px -4px rgba(0, 0, 0, 0.5),
      inset 0 -3px 4px -1px rgba(0, 0, 0, 0.2),
      0 -5px 15px -1px rgba(104, 193, 255, 0.4),
      inset 0 3px 4px -1px rgba(255, 255, 255, 0.2);

    &:after,
    &:before {
      content: "";
      position: absolute;
      background-color: #fff;
      border-radius: 4px;
      transition: transform 1.25s cubic-bezier(0.8, 0, 0.2, 1);
    }

    &:after {
      width: 40px;
      height: 8px;
      top: 35px;
      left: 20px;
    }
    &:before {
      width: 8px;
      height: 40px;
      top: 20px;
      left: 35px;
    }
  }
  ul {
    position: absolute;
    top: 20%;
    left: 50%;
   
  }
  li {
    transition: transform 1s cubic-bezier(0.7, 0, 0.3, 1);

    a {
      position: absolute;
      width: 40px;
      height: 40px;
      line-height: 42px;
      background-color:rgb(250,119,100);
      border-radius: 50%;
      top: 40%;
      left: 50%;
      transform: translate(-100%, -140%);
      transition: background-color 0.5s;

      .fa {
        color: #fff;
      }

      &:hover {
        background-color: #ff590d;
      }
    }
  }
}
</style>
