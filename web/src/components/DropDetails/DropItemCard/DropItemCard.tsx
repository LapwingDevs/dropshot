import React from 'react';
import { Button, Card, Typography } from '@mui/material';
import { DropItemDto } from '../../../api/models/Drops/DropItemDto';
import './DropItemCard.scss';

interface DropItemProps {
  dropItem: DropItemDto;
  addItemToUserCart: () => void;
  reserved?: boolean;
}

const DropItemCard = ({ dropItem, addItemToUserCart, reserved = false }: DropItemProps) => {
  return (
    <Card className="drop-item-container">
      <Typography>{dropItem.productName}</Typography>
      <Typography>{dropItem.size}</Typography>
      {reserved === false && <Button onClick={addItemToUserCart}>Add to cart</Button>}
    </Card>
  );
};

export default DropItemCard;
