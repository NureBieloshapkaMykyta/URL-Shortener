import React, { useEffect, useState } from 'react';
import './About.css';

export const About: React.FC = () => {
    const [user]
  return (
    <div className='form-container '>
      <h2>Details URL Form</h2>
      <form>
        <div>
          <label>Base URL: </label>
          <p  style={{overflow:'auto'}}><a href={detailsUrl?.baseUrl}>{detailsUrl?.baseUrl}</a></p>
          
        </div><br />
        <div>
          <label>Shortered URL: </label>
          <a href={detailsUrl?.shorteredUrl}>{detailsUrl?.shorteredUrl}</a>
        </div><br />
        <div>
          <label>Modified Date: </label>
          <span>{detailsUrl?.modifiedDate}</span>
        </div><br />
        <div>
          <label>Creator: </label>
          <span>{detailsUrl?.creator.username}</span>
        </div><br />
        <div>
          <button style={{backgroundColor:'red'}} onClick={deleteHandler}>Delete</button>
          {deleteError!=="" ? <p>{deleteError}</p> : null}
        </div>
      </form>
    </div>
  );
};