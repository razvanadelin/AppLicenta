import axios from 'axios';

const API_URL = 'http://localhost:5259/api/MealPlan';

const getMealPlan = (userId) => {
  return axios.get(`${API_URL}/user/${userId}`);
};

const getMealPlanByCalories = (calories) => {
  return axios.get(`${API_URL}/calories/${calories}`);
};

const addMealPlan = (mealPlan) => {
  return axios.post(API_URL, mealPlan);
};

export default {
  getMealPlan,
  getMealPlanByCalories,
  addMealPlan,
};
