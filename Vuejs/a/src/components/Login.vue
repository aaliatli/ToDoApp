<template>
  <main id="login">
    <div class="login">
      <h1 style="color: #2d2d29;">Giriş yap</h1>
      <form @submit.prevent="login" class="form-login">
        <input type="text" v-model="Username" placeholder="Kullanıcı adı" required autocomplete="off"><br>
        <input type="password" v-model="Password" placeholder="Şifre" required autocomplete="off"><br>
        <button type="submit">Giriş</button>
      </form>
    </div>
  </main>
</template>
<script>
import axios from 'axios';

export default {
  name: 'Login',
  data() {
    return {
      model:{
        username: '',
        password: '',
      }
    };
  },
  methods: {
    async login() {
      this.model.username = this.Username
      this.model.password = this.Password
      try {
        const response = await axios.post('http://localhost:5001/api/auth/login', this.model);
        localStorage.setItem('currentUser', JSON.stringify(response.data));
        this.$router.push('/todolist');
      } catch (error) {
        console.error('Login error:', error.response ? error.response.data : error.message);
        alert("Kullanıcı adı veya şifre hatalı");
        }

    }
  }
};
</script>

<style>

.login{
    padding: 17%;
    margin-left: 21%;
    font-size: large;
    flex-direction: column;
    align-items: center;
    justify-content: center;

}

.login input{
    width: 300px;
    height: 30px;
    margin-bottom: 10px; 
    border-color: red;
    border-radius: 5px;
}
.login button {
    color: red;
    background-color: white;
    border: none;
    font-size: medium;
    margin-top: 10px; 
    padding: 10px;
}

button:hover {
    border: 1px solid red;
    cursor: pointer;
}


</style>