import React, { useState } from 'react';
import { NavLink, Navigate } from 'react-router-dom';
import "./Sign-up.css"
import { account } from '../../api/Agent';

const SignUp: React.FC = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<boolean>(false);

  const handleUsernameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(e.target.value);
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError(null);
    account.signUp({ username, password })
      .then(() => setSuccess(true))
      .catch((error) => {
        setError(error.response.data || 'An error occurred during sign-up.');
      });
  };

  if (success) {
    return <Navigate to="/Login" />
  }

  return (
    <div className="signup-container">
      <form className="signup-form" onSubmit={handleSubmit}>
        <h2>Sign Up</h2>
        {error && <div className="error">{error}</div>}
        <div className="form-group">
          <label htmlFor="username">Username</label>
          <input type="text" id="username" onChange={handleUsernameChange} />
        </div>
        <div className="form-group">
          <label htmlFor="password">Password</label>
          <input type="password" id="password" onChange={handlePasswordChange} />
        </div>
        <div className="button-container">
          <button type="submit">Sign Up</button>
        </div>
        <div className="link-container">
          <p>Already have an account? <NavLink to="/Login" className="link">Login</NavLink></p>
        </div>
      </form>
    </div>
  );
};

export default SignUp;