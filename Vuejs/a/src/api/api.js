// src/api/api.js

import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5001', // .NET Core API'nin URL'si
  headers: {
    'Content-Type': 'application/json'
  }
});

export default {
  getWeather() {
    return apiClient.get('/weatherforecast');
  },
  login(loginModel) {
    return apiClient.post('api/auth/login',loginModel);
  },
  register(){
    return apiClient.post('api/auth/register',registerModel);
  },
  todoapp(){
    return apiClient.post('api/todo');
  }
};
