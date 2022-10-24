import React from 'react';
import { DropCardDto } from '../../../api/models/Drops/DropCardDto';
import './DropCard.scss';

interface DropCardProps {
  drop: DropCardDto;
}

const DropCard = ({ drop }: DropCardProps) => {
  return <div>{drop.name}</div>;
};

export default DropCard;
