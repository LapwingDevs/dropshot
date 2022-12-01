import React, { useCallback, useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import AuthContext from '../../contexts/AuthContext';

const UnauthorizedRoute = () => {
  const { currentUser } = useContext(AuthContext);

  useCallback(() => {
    if (currentUser && sessionStorage.getItem('token') !== null) {
      return <Navigate to={'/drops'} replace />;
    }
  }, [currentUser]);

  return <Outlet />;
};

export default UnauthorizedRoute;
