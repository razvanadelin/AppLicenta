import React, { useState, useEffect } from 'react';
import { Box, Typography, TextField, Button } from '@mui/material';
import measurementService from '../services/measurementService';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

const Measurements = () => {
  const [measurements, setMeasurements] = useState([]);
  const [newMeasurement, setNewMeasurement] = useState({
    idMasurare: 0,
    greutate: '',
    inaltime: '',
    circTalie: '',
    circSold: '',
    userID: JSON.parse(localStorage.getItem('user')).userID
  });

  useEffect(() => {
    const fetchMeasurements = async () => {
      try {
        const user = JSON.parse(localStorage.getItem('user'));
        const response = await measurementService.getMeasurements(user.userID);
        if (response.data.$values) {
          setMeasurements(response.data.$values);
        } else {
          setMeasurements(response.data);
        }
      } catch (error) {
        console.error('Failed to fetch measurements', error);
        toast.error('Failed to fetch measurements');
      }
    };
    fetchMeasurements();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewMeasurement((prev) => ({ ...prev, [name]: value }));
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Adaugă zero dacă luna este mai mică de 10
    const day = String(date.getDate()).padStart(2, '0'); // Adaugă zero dacă ziua este mai mică de 10
    return `${year}-${month}-${day}`;
  };

  const handleAddMeasurement = async (e) => {
    e.preventDefault();

    try {
      const user = JSON.parse(localStorage.getItem('user'));

      const measurementToSave = {
        idMasurare: 0, // Setăm IdMasurare la 0
        greutate: parseFloat(newMeasurement.greutate),
        inaltime: parseFloat(newMeasurement.inaltime),
        circTalie: parseFloat(newMeasurement.circTalie),
        circSold: parseFloat(newMeasurement.circSold),
        data: new Date().toISOString(), // Setăm automat data și ora curente
        userID: user.userID,
        user: {
          userID: user.userID,
          username: user.username,
          password: user.password,
          firstName: user.firstName,
          lastName: user.lastName,
          contactNr: user.contactNr,
          email: user.email,
          role: user.role,
          gender: user.gender
        }
      };

      // Verifică ce date sunt trimise în cererea de POST
      console.log('Measurement to save:', measurementToSave);

      await measurementService.addMeasurement(measurementToSave);
      setNewMeasurement({
        idMasurare: 0,
        greutate: '',
        inaltime: '',
        circTalie: '',
        circSold: '',
        userID: user.userID
      });
      const response = await measurementService.getMeasurements(user.userID);
      if (response.data.$values) {
        setMeasurements(response.data.$values);
      } else {
        setMeasurements(response.data);
      }
      toast.success('Measurement added successfully!');
    } catch (error) {
      console.error('Failed to add measurement', error);
      toast.error('Failed to add measurement');
    }
  };

  return (
    <Box sx={{ padding: '20px' }}>
      <ToastContainer />
      <Typography variant="h4" mb="20px">Measurements</Typography>

      {/* Graficul evoluției greutății */}
      <ResponsiveContainer width="100%" height={400}>
        <LineChart data={measurements.map(m => ({ ...m, data: formatDate(m.data) }))}>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="data" />
          <YAxis />
          <Tooltip />
          <Legend />
          <Line type="monotone" dataKey="greutate" stroke="#8884d8" activeDot={{ r: 8 }} />
        </LineChart>
      </ResponsiveContainer>

      {measurements.map((measurement) => (
        <Box key={measurement.idMasurare} sx={{ marginBottom: '20px' }}>
          <Typography>Date: {formatDate(measurement.data)}</Typography>
          <Typography>Weight: {measurement.greutate} kg</Typography>
          <Typography>Height: {measurement.inaltime} cm</Typography>
          <Typography>Waist Circumference: {measurement.circTalie} cm</Typography>
          <Typography>Hip Circumference: {measurement.circSold} cm</Typography>
          <Button
            onClick={() => {
              setNewMeasurement(measurement);
              window.scrollTo({ top: 0, behavior: 'smooth' });
            }}
          >
            Edit
          </Button>
        </Box>
      ))}
      <Box component="form" sx={{ display: 'flex', flexDirection: 'column', gap: '10px' }} onSubmit={handleAddMeasurement}>
        <TextField
          label="Weight (kg)"
          name="greutate"
          value={newMeasurement.greutate}
          onChange={handleInputChange}
        />
        <TextField
          label="Height (cm)"
          name="inaltime"
          value={newMeasurement.inaltime}
          onChange={handleInputChange}
        />
        <TextField
          label="Waist Circumference (cm)"
          name="circTalie"
          value={newMeasurement.circTalie}
          onChange={handleInputChange}
        />
        <TextField
          label="Hip Circumference (cm)"
          name="circSold"
          value={newMeasurement.circSold}
          onChange={handleInputChange}
        />
        <Button type="submit" variant="contained" sx={{ backgroundColor: '#FF2625', '&:hover': { backgroundColor: '#FF0000' } }}>
          {newMeasurement.idMasurare ? 'Update Measurement' : 'Add Measurement'}
        </Button>
      </Box>
    </Box>
  );
};

export default Measurements;
