import React from 'react';
import { CartItemDto } from '../../../api/models/Carts/CartItemDto';
import DateCountdown from '../DateCountdown/DateCountdown';
import './CartItem.scss';

interface CartItemProps {
  item: CartItemDto;
}

const CartItem = ({ item }: CartItemProps) => {
  return (
    <div className="cart-item-wrapper" key={item.itemReservationEndDateTime}>
      <div>
        {item.productName}[{item.variantSize}]
      </div>

      <div>
        <DateCountdown deadline={item.itemReservationEndDateTime} />
      </div>
    </div>
  );
};

export default CartItem;
