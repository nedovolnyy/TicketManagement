import 'bootstrap/dist/css/bootstrap.css'
import React, { Fragment } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import App from './App'
import { AuthProvider } from './context/AuthProvider'
import './i18n'
import { Footer } from './views/Footer'

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');
const root = createRoot(rootElement);

root.render(
  <Fragment>
  <BrowserRouter basename={baseUrl}>
    <AuthProvider>
      <App />
    </AuthProvider>
  </BrowserRouter>
  <Footer></Footer>
  </Fragment>
);
