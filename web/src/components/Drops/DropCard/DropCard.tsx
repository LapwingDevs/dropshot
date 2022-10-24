import React from 'react';
import { DropCardDto } from '../../../api/models/Drops/DropCardDto';
import { useNavigate } from 'react-router-dom';

import './DropCard.scss';

interface DropCardProps {
  drop: DropCardDto;
}

const DropCard = ({ drop }: DropCardProps) => {
  const navigate = useNavigate();

  return <div onClick={() => navigate(`/drop/${drop.id}`)}>{drop.name}</div>;
};

export default DropCard;
