import React, { useState, useEffect } from 'react';
import { Box, Typography, TextField, Button } from '@mui/material';
import mealPlanService from '../services/mealPlanService';
import measurementService from '../services/measurementService';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const calculateCalories = (weight, height, age, gender) => {
  // Formula Harris-Benedict
  let calories;
  if (gender === 'male') {
    calories = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age);
  } else {
    calories = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age);
  }

  // Rotunjire la cea mai apropiată sută
  return Math.round(calories / 100) * 100;
};

const MealPlans = () => {
  const [mealPlan, setMealPlan] = useState(null);
  const [age, setAge] = useState('');
  const [measurements, setMeasurements] = useState([]);
  const user = JSON.parse(localStorage.getItem('user'));

  useEffect(() => {
    const fetchMeasurements = async () => {
      try {
        const user = JSON.parse(localStorage.getItem('user'));
        const response = await measurementService.getMeasurements(user.userID);
        setMeasurements(response.data.$values || response.data);  // Aici tratăm posibilitatea ca răspunsul să fie învelit în `$values`
      } catch (error) {
        console.error('Failed to fetch measurements', error);
        toast.error('Failed to fetch measurements');
      }
    };

    const fetchMealPlan = async () => {
      try {
        const response = await mealPlanService.getMealPlan(user.userID);
        setMealPlan(response.data);
      } catch (error) {
        console.error('Failed to fetch meal plan', error);
        toast.error('Failed to fetch meal plan');
      }
    };

    fetchMeasurements();
    fetchMealPlan();
  }, [user.userID]);

  const handleGenerateMealPlan = async () => {
    if (!Array.isArray(measurements) || measurements.length === 0) {
      toast.error('No measurements found');
      return;
    }

    if (!age) {
      toast.error('Please enter your age');
      return;
    }

    // Selectăm ultima măsurătoare
    const latestMeasurement = measurements.reduce((latest, current) => {
      const latestDate = new Date(latest.data);
      const currentDate = new Date(current.data);
      return currentDate > latestDate ? current : latest;
    });

    const calories = calculateCalories(latestMeasurement.greutate, latestMeasurement.inaltime, age, user.gender);

    try {
      const response = await mealPlanService.getMealPlanByCalories(calories);
      setMealPlan(response.data);
      toast.success('Meal plan generated successfully!');
    } catch (error) {
      console.error('Failed to generate meal plan', error);
      toast.error('Failed to generate meal plan');
    }
  };

  return (
    <Box sx={{ padding: '20px' }}>
      <ToastContainer />
      <Typography variant="h4" mb="20px">Meal Plan</Typography>

      {mealPlan ? (
        <Box>
          <Typography variant="h6">Current Meal Plan:</Typography>
          <Typography>Calories: {mealPlan.nrCalorii}</Typography>
          <Typography>Description: {mealPlan.descrierePlan}</Typography>
        </Box>
      ) : (
        <Typography>No meal plan found. Please generate one.</Typography>
      )}

      <TextField
        label="Age"
        type="number"
        value={age}
        onChange={(e) => setAge(e.target.value)}
        fullWidth
        margin="normal"
      />

      <Button variant="contained" color="primary" onClick={handleGenerateMealPlan} sx={{ marginTop: '20px', backgroundColor: '#FF2625' }}>
        Generate Meal Plan
      </Button>
    </Box>
  );
};

export default MealPlans;
