import React, { useState } from 'react';
import authService from '../services/authService';

const SignUp = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [contactNr, setContactNr] = useState('');
  const [email, setEmail] = useState('');
  const [role, setRole] = useState('');
  const [gender, setGender] = useState('');

  const handleSignUp = async (e) => {
    e.preventDefault();
    try {
      const response = await authService.signUp(username, password, firstName, lastName, contactNr, email, role, gender);
      console.log('Signed up:', response.data);
      // Poți redirecționa utilizatorul la pagina de login sau la o pagină protejată
    } catch (error) {
      console.error('Sign up failed:', error);
    }
  };

  return (
    <form onSubmit={handleSignUp}>
      <div>
        <label>Username:</label>
        <input
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </div>
      <div>
        <label>Password:</label>
        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>
      <div>
        <label>First Name:</label>
        <input
          type="text"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
        />
      </div>
      <div>
        <label>Last Name:</label>
        <input
          type="text"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
        />
      </div>
      <div>
        <label>Contact Number:</label>
        <input
          type="text"
          value={contactNr}
          onChange={(e) => setContactNr(e.target.value)}
        />
      </div>
      <div>
        <label>Email:</label>
        <input
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>
      <div>
        <label>Role:</label>
        <input
          type="text"
          value={role}
          onChange={(e) => setRole(e.target.value)}
        />
      </div>
      <div>
        <label>Gender:</label>
        <input
          type="text"
          value={gender}
          onChange={(e) => setGender(e.target.value)}
        />
      </div>
      <button type="submit">Sign Up</button>
    </form>
  );
};

export default SignUp;
