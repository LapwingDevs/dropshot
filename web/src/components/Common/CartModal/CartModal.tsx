import React from 'react';
import Modal from 'react-modal';
import { useCart } from '../../../contexts/CartContext';
import './CartModal.scss';

interface CartModalProps {
  isOpen: boolean;
  handleClose: () => void;
}

const CartModal = ({ isOpen, handleClose }: CartModalProps) => {
  const { userCart } = useCart();

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
            return (
              <div key={item.itemReservationEndDateTime}>
                {item.productName} {item.variantSize}
              </div>
            );
          })}
      </div>
    </Modal>
  );
};

export default CartModal;
