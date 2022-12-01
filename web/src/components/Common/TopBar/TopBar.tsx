import React, { useCallback, useEffect, useState } from 'react';
import { AppBar, Avatar, Box, Button, IconButton, Toolbar, Typography } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import './TopBar.scss';
import CartModal from '../CartModal/CartModal';
import UserMenu from '../MenuModal/UserMenu';
import AuthContext from '../../../contexts/AuthContext';
import { useCart } from '../../../contexts/CartContext';
import { getUserCart } from '../../../api/controllers/CartsClient';
import AccountCircle from '@mui/icons-material/AccountCircle';
import { ShoppingCart } from '@mui/icons-material';

const TopBar = () => {
  const [cartIsOpen, setCartIsOpen] = useState<boolean>(false);
  const { currentUser } = React.useContext(AuthContext);

  const navigate = useNavigate();

  const { setUserCart } = useCart();

  const fetchUserCart = useCallback(() => {
    getUserCart().then((cart) => {
      setUserCart(cart);
    });
  }, []);

  useEffect(() => {
    fetchUserCart();
  }, [fetchUserCart, cartIsOpen]);

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
          <Typography
            variant="h4"
            className="top-bar-title"
            component="div"
            sx={{ flexGrow: 1, color: '#FFFFFF' }}
            onClick={() => navigate('/')}
          >
            DropShot
          </Typography>

          {currentUser && (
            <>
              <IconButton onClick={() => setCartIsOpen(true)}>
                <Avatar>
                  <ShoppingCart />
                </Avatar>
              </IconButton>

              <UserMenu />
            </>
          )}
        </Toolbar>
      </AppBar>
      <CartModal isOpen={cartIsOpen} handleClose={() => setCartIsOpen(false)} />
    </Box>
  );
};

export default TopBar;
