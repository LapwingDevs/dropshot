import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.scss';
import reportWebVitals from './reportWebVitals';
import App from './App';
import { ThemeProvider } from '@mui/material';
import { theme } from './constants/ThemeMUI';
import Modal from 'react-modal';
import { CartProvider } from './contexts/CartContext';

Modal.setAppElement('#root');

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);

root.render(
  <React.StrictMode>
    <ThemeProvider theme={theme}>
      <CartProvider>
        <App />
      </CartProvider>
    </ThemeProvider>
  </React.StrictMode>,
);

reportWebVitals();
