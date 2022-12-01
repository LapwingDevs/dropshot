import React from 'react';

import './AdminPanel.scss';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import { Button } from '@mui/material';

const AdminPanel = () => {
  const navigate = useNavigate();

  return (
    <div className="admin-panel-container">
      <Button className="item" onClick={() => navigate('users-management')}>
        Users
      </Button>
      <Button className="item" onClick={() => navigate('drops-management')}>
        Drops
      </Button>
      <Button className="item" onClick={() => navigate('products-management')}>
        Products
      </Button>
      <Outlet />
    </div>
  );
};

export default AdminPanel;
