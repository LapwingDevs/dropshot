import React from 'react';
import Modal from 'react-modal';
import './CartModal.scss';

interface CartModalProps {
  isOpen: boolean;
  handleClose: () => void;
}

const CartModal = ({ isOpen, handleClose }: CartModalProps) => {
  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={handleClose}
      className="cart-modal-container"
      overlayClassName="cart-modal-overlay"
      ariaHideApp={true}
    >
      <div>hej modal</div>
    </Modal>
  );
};

export default CartModal;
