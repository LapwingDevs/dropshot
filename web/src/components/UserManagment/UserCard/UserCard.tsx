import React, { useState } from 'react';
import { UserDto } from '../../../api/models/User/UserDto';
import { Button, Card } from '@mui/material';
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
    <Card>
      <div>
        {user.firstName} {user.lastName} {user.email}
      </div>
      <Button onClick={onButtonClicked} disabled={buttonDisabled}>
        {buttonLabel}
      </Button>

      <ConfirmationDialog content={'Are you sure?'} open={openDialog} onClose={handleClose} />
    </Card>
  );
};

export default UserCard;
