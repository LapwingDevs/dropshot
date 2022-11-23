import { Button } from '@mui/material';
import React from 'react';
import CartItem from '../../components/Common/CartItem/CartItem';
import { useCart } from '../../contexts/CartContext';
import './Order.scss';

const Order = () => {
  const { userCart } = useCart();

  return (
    <div className="order-wrapper">
      <div>Cart:</div>
      <div>
        {userCart &&
          userCart.cartItems.map((item) => {
            return <CartItem key={item.itemReservationEndDateTime} item={item} />;
          })}
      </div>
      <Button>Submit order</Button>
    </div>
  );
};

export default Order;
