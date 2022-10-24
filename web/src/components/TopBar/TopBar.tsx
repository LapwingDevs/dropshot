import React from 'react';
import { AppBar, Box, Button, Toolbar, Typography } from '@mui/material';
import { Link } from 'react-router-dom';

const TopBar = () => {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Dropshot
          </Typography>
          <Link to={'/admin-panel'}>Admin panel</Link>
          <Link to={'/account'}>Account</Link>
          <Link to={'/drops'}>Drops</Link>
          <Link to={'/login'}>Login</Link>
          <Link to={'/register'}>Register</Link>
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default TopBar;
