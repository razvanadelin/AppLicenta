import React from 'react';
import './App.css';
import { Route, Routes } from 'react-router-dom';
import { Box } from '@mui/material';
import Home from './pages/Home';
import ExerciseDetail from './pages/ExerciseDetail';
import Navbar from './components/Navbar';
import Footer from './components/Footer';
import Exercises from './components/Exercises';




const App = () => {
  return (
    <Box width = "400px"  sx ={{width: {x1: '1488px'}}} m="auto"> 
    <Navbar/>
    <Routes>
      <Route path="/" element ={ <Home />}/>
      <Route path="/exercise" element ={ <Exercises />}/>
      <Route path="/exercises/exercise/:id" element={<ExerciseDetail />} />
    </Routes>
    <Footer/>
    </Box>
  )
}

export default App;

