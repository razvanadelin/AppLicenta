import React, { useState } from 'react';
import './App.css';
import { Route, Routes, Navigate } from 'react-router-dom';
import { Box } from '@mui/material';
import Home from './pages/Home';
import ExerciseDetail from './pages/ExerciseDetail';
import Navbar from './components/Navbar';
import Footer from './components/Footer';
import Exercises from './components/Exercises';
import Login from './components/Login';
import SignUp from './components/SignUp';
import Measurements from './pages/Measurements';
import MealPlans from './pages/MealPlans';
import Training from './components/Training';
const App = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  return (
    <Box width="400px" sx={{ width: { xl: '1488px' } }} m="auto">
      {isAuthenticated && <Navbar setIsAuthenticated={setIsAuthenticated} />}
      <Routes>
        {!isAuthenticated ? (
          <>
            <Route path="/login" element={<Login setIsAuthenticated={setIsAuthenticated} />} />
            <Route path="/signup" element={<SignUp />} />
            <Route path="*" element={<Navigate to="/login" />} />
          </>
        ) : (
          <>
            <Route path="/" element={<Home />} />
            <Route path="/exercise" element={<Exercises />} />
            <Route path="/exercises/exercise/:id" element={<ExerciseDetail />} />
            <Route path="/measurements" element={<Measurements />} />
            <Route path="/mealplans" element={<MealPlans />} />
            <Route path="/training" element={<Training />} />
            <Route path="*" element={<Navigate to="/" />} />
          </>
        )}
      </Routes>
      {isAuthenticated && <Footer />}
    </Box>
  );
};

export default App;
