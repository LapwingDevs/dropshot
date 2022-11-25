import React from 'react';
import Modal from 'react-modal';
import { useCart } from '../../../contexts/CartContext';
import { Button } from '@mui/material';
import './CartModal.scss';
import CartItem from '../CartItem/CartItem';
import { useNavigate } from 'react-router-dom';

interface CartModalProps {
  isOpen: boolean;
  handleClose: () => void;
}

const CartModal = ({ isOpen, handleClose }: CartModalProps) => {
  const { userCart } = useCart();

  const navigate = useNavigate();

  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={handleClose}
      className="cart-modal-container"
      overlayClassName="cart-modal-overlay"
    >
      <div>cart modal</div>
      <div>
        {userCart &&
          userCart.cartItems.map((item) => {
            return <CartItem key={item.itemReservationEndDateTime} item={item} />;
          })}
      </div>
      <div>
        <Button onClick={() => navigate('/order')}>Order</Button>
      </div>
    </Modal>
  );
};

export default CartModal;
