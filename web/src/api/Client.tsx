import axios from 'axios';
import data from '../config.json';

export default axios.create({
  baseURL: data.ApiUrl,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true,
});

export const noAuthClient = axios.create({
  baseURL: data.ApiUrl,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true,
});
