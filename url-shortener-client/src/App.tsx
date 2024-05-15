import { Fragment } from 'react/jsx-runtime';
import { ReactNode } from 'react';
import Header from './components/header/Header';


function App({children}:{children:ReactNode}) {
  return (
    <Fragment>
      <Header />
      {children}
    </Fragment>
  )
}

export default App
