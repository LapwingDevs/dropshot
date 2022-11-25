import React, { useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import AuthContext from '../../context/AuthContext';
import { Admin } from '../../constants/UserRole';

const AdminRoute = () => {
  const { currentUser, userRole } = useContext(AuthContext);

  if (currentUser === undefined || userRole != Admin) {
    return <> {currentUser === undefined ? <Navigate to={'/login'} replace /> : <Navigate to={'/error'} replace />} </>;
  }

  return <Outlet />;
};

export default AdminRoute;
