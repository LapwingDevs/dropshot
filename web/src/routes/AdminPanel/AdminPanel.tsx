import React from 'react';

import './AdminPanel.scss';
import { Link, Outlet } from 'react-router-dom';

const AdminPanel = () => {
  return (
    <div>
      <Link to={'users-management'}>users</Link>
      <Link to={'drops-management'}>drops</Link>
      <Outlet />
    </div>
  );
};

export default AdminPanel;
