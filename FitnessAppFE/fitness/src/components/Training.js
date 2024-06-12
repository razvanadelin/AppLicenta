import React, { useState, useEffect } from 'react';
import trainingService from '../services/trainingService';
import { Box, Typography, Button, MenuItem, Select } from '@mui/material';
import Logo from '../assets/images/banner1.jpg';

const Training = () => {
  const [nivelFitness, setNivelFitness] = useState('');
  const [scopAntrenament, setScopAntrenament] = useState('');
  const [userID, setUserID] = useState(null);
  const [trainingPlan, setTrainingPlan] = useState(null);

  useEffect(() => {
    const storedUser = JSON.parse(localStorage.getItem('user'));
    if (storedUser && storedUser.userID) {
      setUserID(storedUser.userID);
    }
  }, []);

  const handleGeneratePlan = async () => {
    if (!userID) {
      alert('User ID is not defined.');
      return;
    }

    try {
      const response = await trainingService.generateTrainingPlan(userID, nivelFitness, scopAntrenament);
      console.log('Training plan generated:', response.data);
      setTrainingPlan(response.data);
      alert('Training plan generated successfully!');
    } catch (error) {
      console.error('Failed to generate training plan', error);
      alert('Failed to generate training plan');
    }
  };

  return (
    <Box display="flex" flexDirection={{ xs: 'column', md: 'row' }} alignItems="center" justifyContent="center" minHeight="100vh" sx={{ backgroundColor: '#f9f9f9', gap: 3, padding: 2 }}>
      <Box display="flex" flexDirection="column" alignItems="center" width={{ xs: '100%', md: '50%' }} p="20px" borderRadius="8px" boxShadow="0px 0px 10px rgba(0, 0, 0, 0.1)" sx={{ backgroundColor: '#ffffff' }}>
        <Typography variant="h4" fontWeight="bold" mb="24px">Generate Training Plan</Typography>
        <Select value={nivelFitness} onChange={(e) => setNivelFitness(e.target.value)} displayEmpty fullWidth>
          <MenuItem value="" disabled>Select Fitness Level</MenuItem>
          <MenuItem value="Beginner">Beginner</MenuItem>
          <MenuItem value="Intermediate">Intermediate</MenuItem>
          <MenuItem value="Advanced">Advanced</MenuItem>
        </Select>
        <Select value={scopAntrenament} onChange={(e) => setScopAntrenament(e.target.value)} displayEmpty fullWidth sx={{ mt: 2 }}>
          <MenuItem value="" disabled>Select Training Goal</MenuItem>
          <MenuItem value="slabire">slabire</MenuItem>
          <MenuItem value="bulk">bulk</MenuItem>
          <MenuItem value="lose weight">lose weight</MenuItem>
          <MenuItem value="Strength Building">Strength Building</MenuItem>
          <MenuItem value="Muscle Gain">Muscle Gain</MenuItem>
          <MenuItem value="Fat Loss">Fat Loss</MenuItem>
          <MenuItem value="Lose Weight">Lose Weight</MenuItem>
          <MenuItem value="Build Muscle">Build Muscle</MenuItem>
          <MenuItem value="Improve Endurance">Improve Endurance</MenuItem>
        </Select>
        <Button variant="contained" color="primary" onClick={handleGeneratePlan} sx={{ mt: 3 }}>
          Generate Plan
        </Button>
      </Box>
      <Box width={{ xs: '100%', md: '50%' }} display="flex" justifyContent="center" alignItems="center">
        <img src={Logo} alt="Training Banner" style={{ width: '100%', height: 'auto', borderRadius: '8px', boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.1)' }} />
      </Box>
      {trainingPlan && (
        <Box mt={4} p={3} border="1px solid #ddd" borderRadius="8px" sx={{ backgroundColor: '#fff' }}>
          <Typography variant="h6" mb={2}>Generated Training Plan:</Typography>
          <Typography variant="body1"><strong>Fitness Level:</strong> {trainingPlan.nivelFitness}</Typography>
          <Typography variant="body1"><strong>Training Goal:</strong> {trainingPlan.scopAntrenament}</Typography>
          <Typography variant="body1"><strong>Start Date:</strong> {trainingPlan.dataIncepere}</Typography>
          <Typography variant="body1"><strong>End Date:</strong> {trainingPlan.dataSf}</Typography>
          <Typography variant="h6" mt={2}>Exercises:</Typography>
          {trainingPlan.exerciseTraining.$values.map((et, index) => (
            <Typography key={index} variant="body2">- {et.exercices.numeEx}: {et.exercices.descriere}</Typography>
          ))}
        </Box>
      )}
    </Box>
  );
};

export default Training;
