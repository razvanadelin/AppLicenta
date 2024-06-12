import React, { useState } from 'react';
import authService from '../services/authService';
import { useNavigate } from 'react-router-dom';
import { Box, Typography, TextField, Button } from '@mui/material';

const Login = ({ setIsAuthenticated }) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await authService.signIn(username, password);
      console.log('Logged in:', response.data);
      localStorage.setItem('user', JSON.stringify(response.data)); // Save user to localStorage
      setIsAuthenticated(true); // Set authentication state
      navigate('/'); // Redirect to home page
    } catch (error) {
      console.error('Login failed:', error);
      alert('Login failed. Please check your credentials.');
    }
  };

  return (
    <Box
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
      minHeight="100vh"
      sx={{ backgroundColor: '#f9f9f9' }}
    >
      <Typography variant="h4" fontWeight="bold" mb="24px">
        Login
      </Typography>
      <Box
        component="form"
        onSubmit={handleLogin}
        display="flex"
        flexDirection="column"
        alignItems="center"
        width="300px"
        p="20px"
        borderRadius="8px"
        boxShadow="0px 0px 10px rgba(0, 0, 0, 0.1)"
        sx={{ backgroundColor: '#ffffff' }}
      >
        <TextField
          label="Username"
          variant="outlined"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Password"
          type="password"
          variant="outlined"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          fullWidth
          margin="normal"
        />
        <Button type="submit" variant="contained" color="primary" fullWidth>
          Login
        </Button>
        <Button
          type="button"
          onClick={() => navigate('/signup')}
          color="secondary"
          sx={{ mt: '10px' }}
        >
          Don't have an account? Sign Up
        </Button>
      </Box>
    </Box>
  );
};

export default Login;
