import React, { useState } from 'react';
import { UserDto } from '../../../api/models/User/UserDto';
import { Button, Card, Typography } from '@mui/material';
import ConfirmationDialog from '../../Common/ConfirmationDialog/ConfirmationDialog';

interface AdminCardProps {
  user: UserDto;
  buttonLabel: string;
  onConfirm: () => Promise<void>;
  buttonDisabled: boolean;
}

const UserCard = ({ user, buttonLabel, onConfirm, buttonDisabled }: AdminCardProps) => {
  const [openDialog, setOpenDialog] = useState(false);

  const onButtonClicked = () => {
    setOpenDialog(true);
  };

  const handleClose = async (success: boolean) => {
    setOpenDialog(false);

    if (success) {
      await onConfirm();
    }
  };

  return (
    <Card sx={{ display: 'flex', flexDirection: 'row', padding: '10px', marginTop: '15px' }}>
      <Typography sx={{ marginRight: '10px' }} variant="h6">
        {user.firstName} {user.lastName} {user.email}
      </Typography>
      <Button onClick={onButtonClicked} disabled={buttonDisabled}>
        {buttonLabel}
      </Button>

      <ConfirmationDialog content={'Are you sure?'} open={openDialog} onClose={handleClose} />
    </Card>
  );
};

export default UserCard;
