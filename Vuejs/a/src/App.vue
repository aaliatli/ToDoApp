<template>

  <div class="main">
    <div class="app">
      <Navbar />
    </div>
    
    <div>
      <router-view></router-view>
    </div>

    <div class="footer">
      <Footer></Footer>
    </div>
  </div>

</template>

<script>
import Navbar from './components/Navbar.vue';
import Footer from './components/Footer.vue';
import axios from 'axios';

const API_URL = 'http://localhost/5289/api/todo';

export default {
  async fetchTodoItems() {
    try {
      const response = await axios.get(API_URL);
      return response.data; // Web API'den gelen veriler
    } catch (error) {
      console.error('Error fetching todo items:', error);
      throw error;
    }
  },
  name: 'App',
  data() {
    return {
      posts:[],
    }
  },
  components : {
    Navbar,
    Footer
  },
  mounted() {
    axios.get('').then(response => this.posts == response.data)
  },
  
}
</script>

<style scoped>
.main{
  flex-direction:column;
  justify-content: space-between;
}
</style>
