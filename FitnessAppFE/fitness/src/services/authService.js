import axios from 'axios';

const API_URL = 'http://localhost:5259/api/User'; 

const signUp = (username, password, firstName, lastName, contactNr, email, role, gender) => {
  return axios.post(`${API_URL}/signup`, {
    username,
    password,
    firstName,
    lastName,
    contactNr,
    email,
    role,
    gender
  });
};

const signIn = (username, password) => {
  return axios.post(`${API_URL}/signin`, {
    username,
    password
  });
};

export default {
  signUp,
  signIn
};
