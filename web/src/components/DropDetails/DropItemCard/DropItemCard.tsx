import React from 'react';
import { Button, Card } from '@mui/material';
import { DropItem } from '../../../api/models/Drops/DropItem';
import './DropItemCard.scss';

interface DropItemProps {
  dropItem: DropItem;
}

const DropItemCard = ({ dropItem }: DropItemProps) => {
  return (
    <Card className="drop-item-container">
      <div>{dropItem.id}</div>
      <Button>Add to cart</Button>
    </Card>
  );
};

export default DropItemCard;
