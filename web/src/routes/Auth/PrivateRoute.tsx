import React, { useCallback, useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import AuthContext from '../../contexts/AuthContext';

const PrivateRoute = () => {
  const { currentUser } = useContext(AuthContext);

  useCallback(() => {
    if (!currentUser) {
      return <Navigate to={'/login'} replace />;
    }
  }, [currentUser]);

  return <Outlet />;
};

export default PrivateRoute;
