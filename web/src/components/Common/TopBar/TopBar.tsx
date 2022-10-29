import React, { useState } from 'react';
import { AppBar, Box, Button, Toolbar, Typography } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import './TopBar.scss';
import CartModal from '../CartModal/CartModal';

const TopBar = () => {
  const [cartIsOpen, setCartIsOpen] = useState<boolean>(false);
  const navigate = useNavigate();
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
          <Typography
            className="top-bar-title"
            variant="h6"
            component="div"
            sx={{ flexGrow: 1 }}
            onClick={() => navigate('/drops')}
          >
            Dropshot
          </Typography>

          <Button onClick={() => navigate('/admin-panel')} color="secondary">
            Admin panel
          </Button>
          <Button onClick={() => navigate('/account')} color="secondary">
            Account
          </Button>
          <Button color="secondary" onClick={() => setCartIsOpen(true)}>
            Cart
          </Button>
          <Button onClick={() => navigate('/login')} color="secondary">
            Logout
          </Button>

          <Button onClick={() => navigate('/login')} color="secondary">
            Login
          </Button>
          <Button onClick={() => navigate('/register')} color="secondary">
            Register
          </Button>
        </Toolbar>
      </AppBar>
      <CartModal isOpen={cartIsOpen} handleClose={() => setCartIsOpen(false)} />
    </Box>
  );
};

export default TopBar;
