import React from 'react';

import { Link, Outlet, useNavigate } from 'react-router-dom';
import { Button } from '@mui/material';

const UsersManagement = () => {
  const navigate = useNavigate();

  return (
    <div className="admin-panel-container">
      <Button className="item" onClick={() => navigate('admin-list')}>
        List of admins
      </Button>
      <Button className="item" onClick={() => navigate('add-admin')}>
        Add admin
      </Button>
    </div>
  );
};

export default UsersManagement;
