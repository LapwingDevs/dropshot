import React, { useCallback, useEffect, useState } from 'react';
import { AppBar, Box, Button, Toolbar, Typography } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import './TopBar.scss';
import CartModal from '../CartModal/CartModal';
import MenuModal from '../MenuModal/MenuModal';
import AuthContext from '../../../contexts/AuthContext';
import { useCart } from '../../../contexts/CartContext';
import { getUserCart } from '../../../api/controllers/CartsClient';

const TopBar = () => {
  const [cartIsOpen, setCartIsOpen] = useState<boolean>(false);
  const [menuIsOpen, setMenuIsOpen] = useState<boolean>(false);
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
            className="top-bar-title"
            variant="h6"
            component="div"
            sx={{ flexGrow: 1 }}
            onClick={() => navigate('/drops')}
          >
            DropShot
          </Typography>

          {currentUser && (
            <>
              <Button color="secondary" onClick={() => setCartIsOpen(true)}>
                Cart
              </Button>
              <Button color="secondary" onClick={() => setMenuIsOpen(true)}>
                Menu
              </Button>
            </>
          )}
        </Toolbar>
      </AppBar>
      <CartModal isOpen={cartIsOpen} handleClose={() => setCartIsOpen(false)} />
      <MenuModal isOpen={menuIsOpen} handleClose={() => setMenuIsOpen(false)} />
    </Box>
  );
};

export default TopBar;
