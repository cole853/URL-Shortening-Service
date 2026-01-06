import { createRouter, createWebHistory } from 'vue-router'
import Home from './Components/Home.vue'
import Shortcode from './Components/Shortcode.vue'

const routes = [
  { path: '/',
    name: 'Home',
    component: Home
  },
  { path: '/shortcode/:code',
    name: 'Shortcode',
    component: Shortcode,
    props: true
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: routes,
})

export default router
