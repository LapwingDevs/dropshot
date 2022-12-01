import { Button, Typography } from '@mui/material';
import React from 'react';
import CartItem from '../../components/Common/CartItem/CartItem';
import { useCart } from '../../contexts/CartContext';
import './Order.scss';

const Order = () => {
  const { userCart } = useCart();

  const getSumPriceOfItems = (): number => {
    if (userCart === undefined) {
      return 0;
    }
    let total = 0;

    userCart.cartItems.forEach((item) => {
      total += item.productPrice;
    });

    return total;
  };

  const getShippingCost = (itemsPrice?: number): number => {
    const freeShipmentPrice = 200.0;
    const shipmentCost = 30.0;
    const sumPriceOfItems = itemsPrice ? itemsPrice : getSumPriceOfItems();

    if (sumPriceOfItems > freeShipmentPrice) {
      return 0;
    }

    return shipmentCost;
  };

  const getTotalPrice = (): number => {
    console.log('XD');
    const sumPriceOfItems = getSumPriceOfItems();
    const ship = getShippingCost(sumPriceOfItems);
    return sumPriceOfItems + ship;
  };

  return (
    <div className="order-wrapper">
      <Typography variant="h5" sx={{ marginBottom: '15px' }}>
        Cart:
      </Typography>
      <div>
        {userCart &&
          userCart.cartItems.map((item) => {
            return <CartItem key={item.itemReservationEndDateTime} item={item} />;
          })}
      </div>
      {userCart && userCart.cartItems.length > 0 && (
        <div>
          <Typography sx={{ marginTop: '15px' }}>Shipping cost: {getShippingCost()} PLN</Typography>
          <Typography sx={{ marginTop: '5px', marginBottom: '15px' }}>Total price: {getTotalPrice()} PLN</Typography>
        </div>
      )}
      <Button disabled={userCart === undefined || userCart.cartItems.length === 0}>Submit order</Button>
    </div>
  );
};

export default Order;
