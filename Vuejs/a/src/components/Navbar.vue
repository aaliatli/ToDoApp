<template>
  <nav class="header">
    <h1><router-link to="/">To Do</router-link></h1>
    <ul class="nav">
      <li v-if="!isLoggedIn"><router-link to="/register">Kayıt Ol</router-link></li>
      <li v-if="!isLoggedIn"><router-link to="/login">Giriş Yap</router-link></li>
      <li v-if="isLoggedIn" @click="handleLogout">Çıkış</li>
    </ul>
  </nav>
</template>

<script>
export default {
  data() {
    return {
      isLoggedIn: !!localStorage.getItem('currentUser'),
    };
  },
  methods: {
    handleLogout() {
      this.isLoggedIn = false;
      localStorage.removeItem('currentUser');
      window.history.pushState(null, null, window.location.href);
      window.onpopstate = function() {
      window.history.go(1);
      };
      this.$router.push('/login');
    },
  },
  watch: {
    $route(to, from) {
      this.isLoggedIn = !!localStorage.getItem('currentUser');
    }
  }
};
</script>

<style>
.header {
  position: fixed;
  top: 0;
  width: 100%;
  background-color: red;
  color: #fff;
  padding: 15px 0;
  display: flex;
  justify-content: space-between;
  align-items: center;
  text-align: center;
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}
.header ul li{
    display: flex;
    margin-right: 25px;
    list-style-type: none;
    cursor: pointer;
}
.header .h1{
  font-size: 27px;
  margin-left: 25px;
}

.header .nav {
  display: flex;
  gap: 15px;
  margin-right: 50px;
}

.header a {
  color: #fff;
  text-decoration: none;
}

.header h1{
  margin-left: 25px;
}

</style>
  