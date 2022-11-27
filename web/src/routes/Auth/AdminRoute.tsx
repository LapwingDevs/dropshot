import React, { useCallback, useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import AuthContext from '../../contexts/AuthContext';
import { Admin } from '../../constants/UserRole';

const AdminRoute = () => {
  const { currentUser, userRole } = useContext(AuthContext);

  useCallback(() => {
    if (currentUser === undefined || userRole != Admin) {
      return (
        <> {currentUser === undefined ? <Navigate to={'/login'} replace /> : <Navigate to={'/error'} replace />} </>
      );
    }
  }, [currentUser, userRole]);

  return <Outlet />;
};

export default AdminRoute;
