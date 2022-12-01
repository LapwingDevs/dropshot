import { Avatar, Box, Button, IconButton, Menu, MenuItem, Tooltip, Typography } from '@mui/material';
import AccountCircle from '@mui/icons-material/AccountCircle';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import './UserMenu.scss';
import AuthContext from '../../../contexts/AuthContext';
import { Admin } from '../../../constants/UserRole';

const UserMenu = () => {
  const { signOut, userRole } = React.useContext(AuthContext);

  const [anchorElement, setAnchorElement] = React.useState<null | HTMLElement>(null);

  const handleOpenMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElement(event.currentTarget);
  };

  const handleCloseMenu = () => {
    setAnchorElement(null);
  };

  const navigate = useNavigate();

  const navigateToMenuItem = (path: string) => {
    navigate(path);
    handleCloseMenu();
  };

  const logOut = () => {
    signOut();
    handleCloseMenu();
    navigate('/login');
  };

  return (
    <Box sx={{ flexGrow: 0 }}>
      <Tooltip title="Open settings">
        <IconButton onClick={handleOpenMenu} sx={{ p: 0 }}>
          <Avatar>
            <AccountCircle />
          </Avatar>
        </IconButton>
      </Tooltip>
      <Menu
        sx={{ mt: '45px' }}
        id="menu-appbar"
        anchorEl={anchorElement}
        anchorOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        keepMounted
        transformOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        open={Boolean(anchorElement)}
        onClose={handleCloseMenu}
      >
        {userRole == Admin && (
          <MenuItem onClick={() => navigateToMenuItem('/admin-panel')}>
            <Typography textAlign="center">Admin panel</Typography>
          </MenuItem>
        )}

        <MenuItem onClick={() => navigateToMenuItem('account')}>
          <Typography textAlign="center">Account</Typography>
        </MenuItem>

        <MenuItem onClick={() => logOut()}>
          <Typography textAlign="center">Log out</Typography>
        </MenuItem>
      </Menu>
    </Box>
  );

  /*  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={handleClose}
      className="menu-modal-container"
      overlayClassName="menu-modal-overlay"
    >
      <div>
        <div>
          {userRole === Admin && (
            <Button onClick={() => navigateToMenuItem('/admin-panel')} color="secondary">
              Admin panel
            </Button>
          )}
        </div>
        <div>
          <Button onClick={() => navigateToMenuItem('/account')} color="secondary">
            Account
          </Button>
        </div>
        <div>
          <Button onClick={() => logOut()} color="secondary">
            Logout
          </Button>
        </div>
      </div>
    </Modal>
  );*/
};

export default UserMenu;
