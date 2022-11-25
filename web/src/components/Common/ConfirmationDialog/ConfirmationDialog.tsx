import React from 'react';
import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@mui/material';

interface ConfirmationDialogProps {
  title?: string;
  content: string;
  open: boolean;
  onClose: (success: boolean) => void;
}

const ConfirmationDialog = (props: ConfirmationDialogProps) => {
  const { onClose, open, content, title, ...other } = props;

  return (
    <Dialog open={open} {...other}>
      {title && <DialogTitle id="alert-dialog-title">{title}</DialogTitle>}
      <DialogContent>
        <DialogContentText id="alert-dialog-description">{content}</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={() => onClose(false)}>No</Button>
        <Button onClick={() => onClose(true)} autoFocus>
          Yes
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ConfirmationDialog;
