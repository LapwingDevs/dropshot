import React, { useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import AuthContext from '../../context/AuthContext';

const PrivateRoute = () => {
  const { currentUser } = useContext(AuthContext);

  if (!currentUser) {
    return <Navigate to={'/login'} replace />;
  }

  return <Outlet />;
};

export default PrivateRoute;