import '@mdi/font/css/materialdesignicons.css'
import 'bootstrap'
import { createApp } from 'vue'
// @ts-ignore
import App from './App.vue'
import { registerGlobalComponents } from './registerGlobalComponents'
import { router } from './router'
import MasonryWall from "@yeger/vue-masonry-wall";
import { VueMasonryPlugin } from "vue-masonry";

const root = createApp(App)
registerGlobalComponents(root)

root
  .use(router)
  .use(MasonryWall)
  .use(VueMasonryPlugin)
  .mount('#app')
