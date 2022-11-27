import React, { useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import AuthContext from '../../contexts/AuthContext';

const UnauthorizedRoute = () => {
  const { currentUser } = useContext(AuthContext);

  if (currentUser) {
    return <Navigate to={'/drops'} replace />;
  }

  return <Outlet />;
};

export default UnauthorizedRoute;
