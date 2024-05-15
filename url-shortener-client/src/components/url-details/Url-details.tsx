import React, { useEffect, useState } from 'react';
import './Url-details.css';
import { url } from '../../api/Agent';
import { Navigate, useParams } from 'react-router';

interface UrlDetailsProps {
  id: string
}

export const UrlDetails: React.FC<UrlDetailsProps> = ({ id }) => {
  const [detailsUrl, setDetailsUrl] = useState<DetailsUrl>();
  const [redirect, setRedirect] = useState<boolean>(false);
  const [permissionToDelete, setPermissionToDelete] = useState<boolean>(false);
  const [deleteError, setDeleteError] = useState<string>("");

  useEffect(() => {
    url.getById(id).then(response => setDetailsUrl(response));
    url.permissionToDelete(id).then(()=>setPermissionToDelete(true)).catch(()=>setPermissionToDelete(false))
  }, [])

  const deleteHandler = () => {
    if(detailsUrl != undefined){
      url.delete(detailsUrl.id).catch(error=>setDeleteError(error.response.data));
      if(deleteError===""){
        setRedirect(true)
      }
      else{
        setRedirect(false)
      }
    }
  }

  if(redirect){
    return <Navigate to={"/"} />
  }

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
          {permissionToDelete?<button style={{backgroundColor:'red'}} onClick={deleteHandler}>Delete</button>:null}
          {deleteError!=="" ? <p>{deleteError}</p> : null}
        </div>
      </form>
    </div>
  );
};

export const UrlDetailsWrapper: React.FC = () => {
  const { urlid } = useParams();

  if (urlid != undefined) {
    return <UrlDetails id={urlid} />;
  }

  return <UrlDetails id={"default"} />;
};
