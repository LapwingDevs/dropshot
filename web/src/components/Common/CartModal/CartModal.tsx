import React from 'react';
import Modal from 'react-modal';
import { useCart } from '../../../contexts/CartContext';
import DateCountdown from '../DateCountdown/DateCountdown';
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
              <div className="cart-item-wrapper" key={item.itemReservationEndDateTime}>
                <div>
                  {item.productName}[{item.variantSize}]
                </div>

                <div>
                  <DateCountdown deadline={item.itemReservationEndDateTime} />
                </div>
              </div>
            );
          })}
      </div>
    </Modal>
  );
};

export default CartModal;
