import React, { useState } from 'react';
import { AppBar, Box, Button, Toolbar, Typography } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import './TopBar.scss';
import CartModal from '../CartModal/CartModal';
import MenuModal from '../MenuModal/MenuModal';

const TopBar = () => {
  const [cartIsOpen, setCartIsOpen] = useState<boolean>(false);
  const [menuIsOpen, setMenuIsOpen] = useState<boolean>(false);
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
            DropShot
          </Typography>

          <Button color="secondary" onClick={() => setCartIsOpen(true)}>
            Cart
          </Button>
          <Button color="secondary" onClick={() => setMenuIsOpen(true)}>
            Menu
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
      <MenuModal isOpen={menuIsOpen} handleClose={() => setMenuIsOpen(false)} />
    </Box>
  );
};

export default TopBar;
