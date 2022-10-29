import React from 'react';
import { Button, Card } from '@mui/material';
import { DropItemDto } from '../../../api/models/Drops/DropItemDto';
import './DropItemCard.scss';

interface DropItemProps {
  dropItem: DropItemDto;
}

const DropItemCard = ({ dropItem }: DropItemProps) => {
  return (
    <Card className="drop-item-container">
      <div>{dropItem.productName}</div>
      <div>{dropItem.size}</div>
      <Button>Add to cart</Button>
    </Card>
  );
};

export default DropItemCard;
