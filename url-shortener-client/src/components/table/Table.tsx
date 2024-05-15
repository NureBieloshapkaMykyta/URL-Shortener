import React, { useEffect, useState } from 'react';
import './Table.css';
import Description from '../description/Decription';
import AddUrl from '../add-url/Add-url';
import { jwt, setJwt, url } from '../../api/Agent';
import { NavLink } from 'react-router-dom';

const Table: React.FC = () => {
  const [urls, setUrls] = useState<DisplayUrl[]>([]);
  const [isAuth, setIsAuth] = useState<boolean>(false);

  useEffect(() => {
    const token = jwt();
    if (token != undefined && token !== "none") {
      setJwt(token);
      setIsAuth(true);
    }
    else{
      setIsAuth(false)
    }

    url.get().then(response => {
      setUrls(response);
    });
  }, [urls])

  return (
    <div className="table-wrapper">
      <Description />
      {isAuth ? (<AddUrl />) : null}      
      <div className='table-container'>
        <table className="table">
          <thead>
            <tr>
              <th>Original URL</th>
              <th>Shortered URL</th>
              <th>Options</th>
            </tr>
          </thead>
          <tbody>
            {urls.map((url) => (
              <tr key={url.id}>
                <td style={{overflow:'auto'}}>{url.baseUrl}</td>
                <td>{url.shorteredUrl}</td>
                <td>{isAuth ? <NavLink to={'/' + url.id}>Details</NavLink> : null}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

    </div>
  );
};

export default Table;