<template>
  <main id="app">
    <div class="form-list">
      <h1>To Do</h1>
      <form @submit.prevent="addItem">
        <input type="text" name="newitem" id="newitem" v-model="itemtitle" placeholder="To Do" autocomplete="off" required><br>
        <input type="datetime-local" name="date" v-model="itemdate"><br>
        <input type="text" name="description" id="desc" v-model="itemdesc" placeholder="Açıklama" autocomplete="off"><br>
        <button type="submit">Ekle</button>
      </form>
    </div>
    <div>
      <div class="items" v-if="items.length">
        <ul>
          <li v-for="(item, index) in items" :key="item.id" :class="{'done': item.done}">
            <span class="label" :class="{'done-label': item.done}"> <b> Başlık: </b>{{ item.title }}</span><br>
            <span class="label" :class="{'done-label': item.done}"> <b> Tarih: </b>{{ item.date }}</span><br>
            <span class="label" :class="{'done-label': item.done}"> <b> Açıklama: </b> {{ item.description }}</span><br>
            <div class="actions">
              <div v-if="showMessage" class="notification">
                {{ message }}
              </div>
              <button class="btn-picto" type="button" @click="updateItem(item.id)">
                {{ item.done ? 'Geri Al' : 'Tamamlandı' }}
              </button>
              <button class="btn" type="button" aria-label="Delete" title="Delete" @click="deleteItem(item.id)">Sil</button>
            </div>
          </li>
        </ul>
      </div>

      <p class="emptylist" v-else>To Do List mevcut değil!</p>


      <div class="itemCount">
        <p>Öğe sayısı: {{ itemCount() }}</p>&#160
        <p> &#160 Tamamlanan öğe sayısı: {{ completedItemCount }} </p> 
      </div>

    </div>

  </main>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ToDoList',
  data() {
    return {
      items: [],
      itemtitle: '',
      itemdate: '',
      itemdesc: '',
      showMessage: false,
      message: ''
    };
  },
  created() {
    this.loadItems();
  },
  methods: {
    async loadItems() {
      const userId = localStorage.getItem('currentUser');
      const savedItems = localStorage.getItem(`todo-items-${userId}`);
      if (savedItems) {
        this.items = JSON.parse(savedItems);
      }
    },
    async saveItems() {
      const userId = localStorage.getItem('currentUser');
      localStorage.setItem(`todo-items-${userId}`, JSON.stringify(this.items));
    },
    async updateItem(id) {
      const item = this.items.find(item => item.id === id);
      if (item) {
        item.done = !item.done; 
        try {
          await axios.put(`http://localhost:5001/api/todo/${id}`, item);
          this.saveItems();
          this.showMessage = true;
          this.message = item.done ? 'Öğe tamamlandı!' : 'Öğe geri alındı!';
          setTimeout(() => {
            this.showMessage = false;
          }, 3000);
        } catch (error) {
          console.log("Todo update error:", error.response ? error.response.data : error.message);
        }
      }
    },
    async deleteItem(id) {
      const index = this.items.findIndex(item => item.id === id);
      if (index !== -1) {
        try {
          await axios.delete(`http://localhost:5001/api/todo/${id}`);
          this.items.splice(index, 1);
          this.saveItems();
        } catch (error) {
          console.log("Todo delete error:", error.response ? error.response.data : error.message);
        }
      }
    },
    async addItem() {
      const newItem = {
        title: this.itemtitle,
        date: this.itemdate,
        description: this.itemdesc,
        done: false
      };

      try {
        const response = await axios.post('http://localhost:5001/api/todo/newtodo', newItem);
        const addedItem = response.data;
        
        if (this.itemtitle.trim() !== '' && this.itemdesc.trim() !== '' && this.itemdate.trim() !== '') {
          this.items.push(addedItem);
          this.itemtitle = '';
          this.itemdate = '';
          this.itemdesc = '';
          this.saveItems();
          this.showMessage = true;
          this.message = 'Yeni öğe eklendi!';
          setTimeout(() => {
            this.showMessage = false;
          }, 3000);
        } else {
          alert('Tüm alanlar doldurulmalıdır.');
        }
      } catch (error) {
        console.log("Todo add error:", error.response ? error.response.data : error.message);
      }
    },
    itemCount(){
      return this.items.length;
    },

  },
  computed: {
    completedItemCount() {
      return this.items.filter(item => item.done).length;
    }
  }
};
</script>

<style scoped>
/* Stil kodları buraya */
</style>



<style scoped>
main{
  width: 80%;
  gap: 1%;
}
/* sol */
.form-list{
  background-color: red;
  color: white;
  width: 63%;
  padding-top: 20%;
  padding-bottom: 20%;
  font-size: large;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  text-align: center;
}
.form-list button{
  width: 80px;
  height: 40px;
} 
.form-list button :hover{
  border-color: white;
}
.form-list form input{
  width: 300px;
  height: 30px;
  border-radius: 5px;
  margin-bottom: 10px;
}
/* Sağ */
.items {
  color: red;
  padding: 50px;
  font-size: large;
  overflow-y: auto; /* Dikey scrollbar */
  height: 60vh; /* İstediğiniz maksimum yükseklik */
  width: 100%; /* İstediğiniz genişlik */
  margin-left: 100px;
  margin-top: 100px;
  border: 1px solid gray;
}
.itemCount{
  top: 0;
  margin-left: 23%;
}
.itemCount p{
  font-weight: 400;
  color: gray;
  
}
.items ul {
  padding: 0;
  align-items: center;
  text-align: center;
  justify-content: center;
  margin: 0;
  width: 100%;
}
.items li {
  list-style: none;
  padding: 10px;
  border: 1px solid red;
  margin-bottom: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%; /* İçerik genişliği tam sayfa genişliği kadar */
}

.items .actions {
  display: flex;
  gap: 10px;
}
.items .btn-picto,
.items .btn {
  cursor: pointer;
  outline: none;
}
.emptylist {
  padding: 10px;
  border: 1px solid red;
  margin-bottom: 10px;
  width: 300px;
  display: flex;
  flex-direction: column;
  align-items: center;
}
.notification {
  position: fixed;
  bottom: 20px;
  right: 20px;
  background-color: #ffcfc8;
  color: white;
  padding: 15px;
  border-radius: 5px;
}
.done-label {
  text-decoration: line-through;
}

.itemCount{
  display:flex;
}

</style>