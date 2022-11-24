import React from 'react';
import { Button, Card } from '@mui/material';
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
      <div>{dropItem.productName}</div>
      <div>{dropItem.size}</div>
      {reserved === false && <Button onClick={addItemToUserCart}>Add to cart</Button>}
    </Card>
  );
};

export default DropItemCard;
