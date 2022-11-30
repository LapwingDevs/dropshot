import React from 'react';
import { DropCardDto } from '../../../api/models/Drops/DropCardDto';
import { useNavigate } from 'react-router-dom';

import './DropCard.scss';
import { Button, Card, Typography } from '@mui/material';
import { DropStatus } from '../../../constants/Drops';
import { format } from 'date-fns';
import { appDateFormat } from '../../../constants/Dates';

interface DropCardProps {
  drop: DropCardDto;
  dropStatus: DropStatus;
}

const DropCard = ({ drop, dropStatus }: DropCardProps) => {
  const navigate = useNavigate();

  return (
    <Card className="drop-card-container" onClick={() => navigate(`${drop.id}`)}>
      <Typography>{drop.name}</Typography>
      <Typography>
        <span>{dropStatus === DropStatus.IncomingDrop ? 'Start: ' : 'End: '}</span>
        <span>
          {format(
            new Date(dropStatus === DropStatus.IncomingDrop ? drop.startDateTime : drop.endDateTime),
            appDateFormat,
          )}
        </span>
      </Typography>
    </Card>
  );
};

export default DropCard;
