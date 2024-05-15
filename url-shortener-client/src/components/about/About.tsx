import React, { SyntheticEvent, useEffect, useState } from 'react';
import './About.css';
import { about, account, roles } from '../../api/Agent';

export const About: React.FC = () => {
    const [user, setUser] = useState<DetailsUser>({username:"", role:1});
    const [description, setDescription] = useState<AlhoritmInfo>({id:'',name:'',description:''});
    const [update, setUpdate] = useState<boolean>(false);
    const [inputDescription, setInputDescription] = useState<string>("");

    useEffect(() => {
      account.details().then(response=>setUser(response));
      about.get().then(response=>setDescription(response));
    }, [])

    const updateHandler = () => {
      setUpdate(!update);
    }

    const descriptionChangeHandler =(e:any) => {
      setInputDescription(e.target.value)
    }

    const updateDescriptionHandler = () => {
      about.update(description.id,{description:inputDescription}).then(()=>{
        setUpdate(false);
      })
    }

  return (
    <div className='form-container '>
      <h2>Details URL Form</h2>
      {update?
      (<div><input style={{marginBottom:'10px'}} type="text" placeholder={description?.description} onChange={e=>descriptionChangeHandler(e)} /><button style={{marginBottom:'20px'}} onClick={updateDescriptionHandler}>Save</button></div> ):
      (<p>{description?.description}</p>)}
      {roles[user.role]==="Admin" ? (<button onClick={updateHandler}>Update</button>) : null}
    </div>
  );
};