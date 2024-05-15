import React, { useEffect, useState } from 'react';
import './Header.css';
import { NavLink } from 'react-router-dom';
import { jwt, setJwt } from '../../api/Agent';

const Header: React.FC = () => {
  const [isAuth, setIsAuth] = useState<boolean>(false);

  useEffect(() => {
    const token = jwt();
    if (token !== "none") {
      setIsAuth(true);
    }
    else{
      setIsAuth(false)
    }
  }, [isAuth])

  const logoutClickHandler = () => {
    const headers = new Headers();
    headers.append('Content-Type', 'application/json'); 
    headers.append('Authorization', 'Bearer '+ jwt()); 
    
    const requestOptions = {
      method: 'GET',
      headers: headers
    };

    fetch("https://localhost:7239/Account/Logout", requestOptions)
    setJwt("none");
    setIsAuth(false);
  }

  return (
    <header className="header">
      <div className='options-container'>
        <nav>
          <ul className='nav-list'>
            <li><NavLink to="/">Home</NavLink></li>
          </ul>
        </nav>
        <nav>
        {isAuth ?
          (<ul className="nav-list">
            <li onClick={logoutClickHandler}>Logout</li>
          </ul>
          ) :
          (<ul className="nav-list">
            <li><NavLink to="/Login">Login</NavLink></li>
            <li><NavLink to="/Sign-up">Sign-up</NavLink></li>
          </ul>
          )}
      </nav>
      </div>
      
    </header>
  );
};

export default Header;