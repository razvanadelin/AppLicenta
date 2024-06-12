import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Stack, Button } from '@mui/material';
import Logo from '../assets/images/Logo.png';



const Navbar = ({ setIsAuthenticated }) => {
  const navigate = useNavigate();

  const handleLogout = () => {
    setIsAuthenticated(false);
    localStorage.removeItem('user');
    navigate('/login');
  };

  return (
    <Stack direction="row" justifyContent="space-around" sx={{ gap: { sm: '122px', xs: '40px' }, mt: { sm: '32px', xs: '20px' }, justifyContent: 'none' }} px="20px">
      <Link to="/">
        <img src={Logo} alt="logo" style={{ width: '150px', height: '150px', margin: '0 20px' }} />
      </Link>
      <Stack direction="row" gap="40px" fontSize="24px" alignItems="flex-end">
        <Link to="/" style={{ textDecoration: 'none', color: '#3A1212', borderBottom: '3px solid #FF2625' }}>Home</Link>
        <a href="#exercises" style={{ textDecoration: 'none', color: '#3A1212' }}>Exercises</a>
        <Link to="/measurements" style={{ textDecoration: 'none', color: '#3A1212' }}>Measurements</Link>
        <Link to="/mealplans" style={{ textDecoration: 'none', color: '#3A1212' }}>Meal Plans</Link>
        <Link to="/training" style={{ textDecoration: 'none', color: '#3A1212' }}>Training</Link>
        <Button onClick={handleLogout} style={{ color: '#FF2625' }}>Logout</Button>
      </Stack>
    </Stack>
  );
};

export default Navbar;
