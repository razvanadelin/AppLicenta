import axios from 'axios';

const API_URL = 'http://localhost:5259/api/Training';

const fetchTrainings = async () => {
  return await axios.get(API_URL);
};

const generateTrainingPlan = async (userId, nivelFitness, scopAntrenament) => {
  return await axios.get(`${API_URL}/generate`, {
    params: {
      userId,
      nivelFitness,
      scopAntrenament,
    },
  });
};

export default {
  fetchTrainings,
  generateTrainingPlan,
};
