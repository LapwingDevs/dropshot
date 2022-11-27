import React from 'react';

import { Link, Outlet } from 'react-router-dom';

const UsersManagement = () => {
  return (
    <div>
      <div>
        <Link to={'admin-list'}>List of admins</Link>
      </div>
      <div>
        <Link to={'add-admin'}>Add admin</Link>
      </div>
      <Outlet />
    </div>
  );
};

export default UsersManagement;
