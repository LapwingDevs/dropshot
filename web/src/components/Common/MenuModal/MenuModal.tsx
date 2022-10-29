import { Button } from '@mui/material';
import React from 'react';
import Modal from 'react-modal';
import { useNavigate } from 'react-router-dom';
import './MenuModal.scss';

interface MenuModalProps {
  isOpen: boolean;
  handleClose: () => void;
}

const MenuModal = ({ isOpen, handleClose }: MenuModalProps) => {
  const navigate = useNavigate();

  const navigateToMenuItem = (path: string) => {
    navigate(path);
    handleClose();
  };

  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={handleClose}
      className="menu-modal-container"
      overlayClassName="menu-modal-overlay"
    >
      <div>
        <div>
          <Button onClick={() => navigateToMenuItem('/admin-panel')} color="secondary">
            Admin panel
          </Button>
        </div>
        <div>
          <Button onClick={() => navigateToMenuItem('/account')} color="secondary">
            Account
          </Button>
        </div>
        <div>
          <Button onClick={() => navigateToMenuItem('/login')} color="secondary">
            Logout
          </Button>
        </div>
      </div>
    </Modal>
  );
};

export default MenuModal;
