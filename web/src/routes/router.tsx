import { createBrowserRouter } from 'react-router-dom';
import Drops from './Drops/Drops';
import ErrorPage from './ErrorPage/ErrorPage';
import Login from './Login/Login';
import Register from './Register/Register';
import Root from './Root/Root';

import React from 'react';
import AdminPanel from './AdminPanel/AdminPanel';

export const router = createBrowserRouter([
  {
    path: '/',
    element: <Root />,
    errorElement: <ErrorPage />,
  },
  {
    path: '/login',
    element: <Login />,
  },
  {
    path: '/register',
    element: <Register />,
  },
  {
    path: '/drops',
    element: <Drops />,
  },
  {
    path: '/admin-panel',
    element: <AdminPanel />,
  },
]);
