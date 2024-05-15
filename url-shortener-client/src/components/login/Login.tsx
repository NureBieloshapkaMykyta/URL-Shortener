import React, { useState } from 'react';
import './Login.css';
import { NavLink, Navigate } from 'react-router-dom';
import { account, setJwt } from '../../api/Agent';

const Login: React.FC = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [iaAuth, setIsAuth] = useState<boolean>(false);

  const handleUsernameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(e.target.value);
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError(null); 
    account.login({username, password})
      .then((response) => {
        setIsAuth(true);
        setJwt(response);
      })
      .catch((error) => {
        setError(error.response.data || 'An error occurred during sign-up.'); 
      });
  };

  if(iaAuth){
    return <Navigate to="/" />
  }

  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleSubmit}>
        <h2>Login</h2>
        {error && <div className="error">{error}</div>} 
        <div className="form-group">
          <label htmlFor="username">Username</label>
          <input type="text" id="username" value={username} onChange={handleUsernameChange} />
        </div>
        <div className="form-group">
          <label htmlFor="password">Password</label>
          <input type="password" id="password" value={password} onChange={handlePasswordChange} />
        </div>
        <button type="submit">Login</button>
        <p>Don't have an account? <NavLink to="/Sign-up">Sign Up</NavLink></p>
      </form>
    </div>
  );
};

export default Login;