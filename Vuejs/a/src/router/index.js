import { createRouter, createWebHistory } from 'vue-router';
import Login from '../components/Login.vue';  // Login bileşenini import edin
import Register from '../components/Register.vue';  // Register bileşenini import edin
import ToDoList from '../components/ToDoList.vue';  // ToDoList bileşenini import edin
import Navbar from '@/components/Navbar.vue'

const routes = [
  {
    path: '/',
    redirect: '/login'  // Ana yol için yönlendirme
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/todolist',
    name: 'ToDoList',
    component: ToDoList
  },
  {
    path: '/navbar',
    name: 'Navbar',
    component: Navbar
  },


  // Diğer rotalarınızı buraya ekleyin
];

const router = createRouter({
  history: createWebHistory(),
  routes
});


export default router;
