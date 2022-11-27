import React from 'react';
import { Button, Card } from '@mui/material';
import { DropItemDto } from '../../../api/models/Drops/DropItemDto';
import './DropItemCard.scss';

interface DropItemProps {
  dropItem: DropItemDto;
  addItemToUserCart: () => void;
  addToCardImpossible?: boolean;
}

const DropItemCard = ({ dropItem, addItemToUserCart, addToCardImpossible = false }: DropItemProps) => {
  return (
    <Card className="drop-item-container">
      <div>{dropItem.productName}</div>
      <div>{dropItem.size}</div>
      {addToCardImpossible === false && (
        <Button onClick={addItemToUserCart} style={{ color: 'black' }}>
          Add to cart
        </Button>
      )}
    </Card>
  );
};

export default DropItemCard;
