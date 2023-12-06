import axios from 'axios';

const instance = axios.create({
  baseURL: 'https://localhost:7267/api'
});

export default instance;