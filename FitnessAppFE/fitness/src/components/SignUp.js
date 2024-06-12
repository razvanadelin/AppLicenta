import React, { useState } from 'react';
import authService from '../services/authService';
import { useNavigate } from 'react-router-dom';
import { Box, Typography, TextField, Button } from '@mui/material';

const SignUp = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [contactNr, setContactNr] = useState('');
  const [email, setEmail] = useState('');
  const [role, setRole] = useState('');
  const [gender, setGender] = useState('');
  const navigate = useNavigate();

  const handleSignUp = async (e) => {
    e.preventDefault();
    try {
      const response = await authService.signUp(username, password, firstName, lastName, contactNr, email, role, gender);
      console.log('Signed up:', response.data);
      navigate('/login');
    } catch (error) {
      console.error('Sign up failed:', error);
      if (error.response && error.response.status === 409) {
        alert('Username already exists. Please choose a different username.');
      } else {
        alert('An error occurred during sign up. Please try again.');
      }
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
        Sign Up
      </Typography>
      <Box
        component="form"
        onSubmit={handleSignUp}
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
        <TextField
          label="First Name"
          variant="outlined"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Last Name"
          variant="outlined"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Contact Number"
          variant="outlined"
          value={contactNr}
          onChange={(e) => setContactNr(e.target.value)}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Email"
          type="email"
          variant="outlined"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Role"
          variant="outlined"
          value={role}
          onChange={(e) => setRole(e.target.value)}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Gender"
          variant="outlined"
          value={gender}
          onChange={(e) => setGender(e.target.value)}
          fullWidth
          margin="normal"
        />
        <Button type="submit" variant="contained" color="primary" fullWidth>
          Sign Up
        </Button>
        <Button
          type="button"
          onClick={() => navigate('/login')}
          color="secondary"
          sx={{ mt: '10px' }}
        >
          Already have an account? Login
        </Button>
      </Box>
    </Box>
  );
};

export default SignUp;
