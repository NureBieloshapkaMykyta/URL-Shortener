import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import SignUp from './components/sign-up/Sign-up.tsx'
import Login from './components/login/Login.tsx'
import Table from './components/table/Table.tsx'
import { UrlDetailsWrapper } from './components/url-details/Url-details.tsx'
import { RedirectWrapper } from './components/redirect/Redirect.tsx'
import { About } from './components/about/About.tsx'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <BrowserRouter>
    <App>
      <Routes>
        <Route path='/' element={<Table />} />
        <Route path='/:urlid' element={<UrlDetailsWrapper />} />
        <Route path='/Login' element={<Login/>} />
        <Route path='/About' element={<About/>} />
        <Route path='/Sign-up' element={<SignUp/>}/>
        <Route path='/test-surl/:code' element={<RedirectWrapper />}/>
      </Routes>
    </App>
  </BrowserRouter>
  </React.StrictMode>,
)
