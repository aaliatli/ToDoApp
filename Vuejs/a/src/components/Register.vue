<template>

    <main id="Register">
        <div class="desc">
            <h1>Kayıt ol!</h1>
            <p>Gününü planlamak için ekrana bilgileri gir ve uygulamayı kullanmaya başla</p>
        </div>
        <div class="form-reg">
            <form  @submit.prevent="register">
                
                <input type="text" name="userName" id="userName" placeholder="Kullanıcı ad" v-model="Username" required autocomplete="off"><br>
                <input type="text" name="eMail" id="eMail" placeholder="for@examle.com" v-model="Email" required autocomplete="off"><br>
                <input type="text" name="phone" placeholder="(123) 456 7890" v-model="Phone" required autocomplete="off"><br>
                <input type="text" name="password" id="password" placeholder="Şifre" v-model="Password" required autocomplete="off"><br>
                <button type="submit" @click="">Kayıt Ol</button> 

                <!-- <label for="">Ad: </label>
                <label for="">Soyad: </label>
                <label for="">e-mail: </label>
                <label for="">Telefon: </label>
                <label for="">Şifre: </label>
                <label for="">Şifre Tekrar: </label> -->
            </form>
        </div>
    </main>

</template>

<script>
import axios from 'axios';

export default{
    name:'Register',
    data() {
        return {
            model:{
                username: '',
                email: '',
                phone: '',
                password: '',
            }

        }
    },
    methods: {
    async register() {
        this.model.username = this.Username;
        this.model.password = this.Password;
        this.model.email = this.Email;
        this.model.phone = this.Phone;

        try {
            let users = JSON.parse(localStorage.getItem('users'));
            
            if (!Array.isArray(users)) {
                users = [];
            }

            let existingUser = users.find(u => u.email === this.Email);

            if (existingUser) {
                alert("Bu email hesabına ait bir kullanıcı hesabı mevcut.");
                return;
            }

            const response = await axios.post('http://localhost:5001/api/auth/register', this.model);

            users.push({
                username: this.Username,
                email: this.Email,
                phone: this.Phone,
                password: this.Password,
            });

            localStorage.setItem('users', JSON.stringify(users));

            alert("Kayıt Başarılı!");
            this.$router.push('/login');

            } catch (error) {
                console.error('Register error:', error);
            }
}
}


}

</script>

<style>

main {
    display: flex;
    justify-content: space-between;
    align-items: center;
    text-align: center;
}

main .desc {
    background-color: red;
    color: white;
    padding:20%;
    font-size: large;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    text-align: center;
}

main .form-reg {
    color: red;
    padding: 20px;
    font-size: large;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    gap: 10px; 
    margin-left: 15%;
    margin-right: 11%;
    margin-top: 60px;
    
}

main .form-reg input {
    border-color: red;
    width: 300px;
    height: 30px;
    gap: 5px;
    justify-content: space-around;
    border-radius: 5px;
    margin-bottom: 10px;
}

button {
    color: red;
    background-color: white;
    border: none;
    font-size: medium;
    margin-top: 10px; 
    padding: 10px;
}

button:hover {
    border: 1px solid red;
}

</style>