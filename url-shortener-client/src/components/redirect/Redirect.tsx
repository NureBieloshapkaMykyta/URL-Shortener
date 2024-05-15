import { useParams } from "react-router";
import { url } from "../../api/Agent";
import React, { useEffect, useState } from "react";

interface RedirectProps {
  code: string
}

export const Redirect: React.FC<RedirectProps> = ({ code }) => {
  const [fullUrl, setFullUrl] = useState<string>("");

  useEffect(() => {
    console.log(code);
    url.getFullUrl(code).then(response => {setFullUrl(response);console.log(response)})
    console.log(fullUrl)
  }, [])

  if(fullUrl!==""){
    window.location.href = fullUrl;
  }
  

  return (<></>)
}

export const RedirectWrapper: React.FC = () => {
  const { code } = useParams();

  if (code != undefined) {
    return <Redirect code={code} />;
  }

  return <Redirect code={"default"} />;
};
