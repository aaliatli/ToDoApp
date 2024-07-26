
<template>
    <div class="footer">
      <p>&copy; 2024 My Website. All rights reserved.</p>
      <nav>
        <a href="#">Privacy Policy</a>
        <a href="#">Terms of Service</a>
      </nav>
    </div>   
</template>

<style>
.footer {
  display: flex;
  position:sticky;
  bottom: 0;
  width: 100%;
  color: gray;
  padding: 3px 0;
  border-top: 1px solid gray;
  justify-content: space-between;
  position:static;
  margin-top: auto;
}

.footer nav {
  display: flex;
  gap: 15px;
  margin-top: 15px;
  
}

.footer a{
  color: gray;
}

</style>
