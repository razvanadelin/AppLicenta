import Icon from '../assets/icons/gym.png'
import React from 'react';
import { Stack, Typography } from '@mui/material';

const BodyPart = ({ item, bodyPart, setBodyPart }) => {
  const itemName = item.name || item;

  return (
    <Stack
      type="button"
      alignItems="center"
      justifyContent="center"
      className="bodyPart-card"
      sx={{
        borderTop: bodyPart === itemName ? '4px solid #FF2625' : '',
        backgroundColor: '#fff',
        borderBottomLeftRadius: '20px',
        width: '270px',
        height: '280px',
        cursor: 'pointer',
        gap: '47px',
      }}
      onClick={() => {
        setBodyPart(itemName);
        window.scrollTo({ top: 1800, left: 100, behavior: 'smooth' });
      }}
    >
      <img src={Icon} alt={itemName} style={{ width: '40px', height: '40px' }} />
      <Typography fontSize="24px" fontWeight="bold" color="#3A1212" textTransform="capitalize">
        {itemName}
      </Typography>
    </Stack>
  );
};

export default BodyPart;
