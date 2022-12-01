import React from 'react';
import { CartItemDto } from '../../../api/models/Carts/CartItemDto';
import DateCountdown from '../DateCountdown/DateCountdown';
import './CartItem.scss';
import { Typography } from '@mui/material';

interface CartItemProps {
  item: CartItemDto;
}

const CartItem = ({ item }: CartItemProps) => {
  return (
    <div className="cart-item-wrapper" key={item.itemReservationEndDateTime}>
      <Typography variant="body2">
        {item.productName}[{item.variantSize}]
      </Typography>

      <Typography variant="body2">{item.productPrice} PLN</Typography>

      <Typography variant="body2">
        <DateCountdown deadline={item.itemReservationEndDateTime} />
      </Typography>
    </div>
  );
};

export default CartItem;
