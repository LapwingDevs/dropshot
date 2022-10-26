import React from 'react';

import './AdminPanel.scss';
import { Link, Outlet } from 'react-router-dom';

const AdminPanel = () => {
  return (
    <div>
      <div>
        <Link to={'users-management'}>users</Link>
      </div>
      <div>
        <Link to={'drops-management'}>drops</Link>
      </div>
      <div>
        <Link to={'products-management'}>products</Link>
      </div>
      <Outlet />
    </div>
  );
};

export default AdminPanel;
