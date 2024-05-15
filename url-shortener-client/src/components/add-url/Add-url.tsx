import React, { SyntheticEvent, useState } from 'react';
import './Add-url.css';
import { url } from '../../api/Agent';

const AddUrl: React.FC = () => {
  const [baseUrl, setBaseUrl] = useState<string>("");

  const handleUrlChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setBaseUrl(e.target.value);
  };

  const handleSubmit = (e:SyntheticEvent) => {
    e.preventDefault();
    url.create({baseUrl});
  }

  return (
    <div className="add-url">
        <input className='input-url' type="text" onChange={handleUrlChange} />
        <button className='short-url' onClick={handleSubmit}>Short url</button>
    </div>
  );
};

export default AddUrl;