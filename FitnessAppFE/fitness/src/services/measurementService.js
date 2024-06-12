import axios from 'axios';

const API_URL = 'http://localhost:5259/api/Measurements'; // Asigură-te că aceasta este ruta corectă

const getMeasurements = (userId) => {
  return axios.get(`${API_URL}/user/${userId}`);
};

const addMeasurement = (measurement) => {
  return axios.post(API_URL, measurement);
};

export default {
  getMeasurements,
  addMeasurement,
};
