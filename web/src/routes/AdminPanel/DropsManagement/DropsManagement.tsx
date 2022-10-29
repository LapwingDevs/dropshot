import { Button } from '@mui/material';
import { format } from 'date-fns';
import React, { useCallback, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getDropsWithDetails } from '../../../api/controllers/DropsClient';
import { AdminDropDto } from '../../../api/models/Drops/AdminDropDto';
import { appDateFormat } from '../../../constants/Dates';

const DropsManagement = () => {
  const [drops, setDrops] = useState<AdminDropDto[]>([]);
  const navigate = useNavigate();

  const fetchDrops = useCallback(() => {
    getDropsWithDetails().then((d) => {
      console.log(d);
      setDrops(d);
    });
  }, []);

  useEffect(() => {
    fetchDrops();
  }, [fetchDrops]);

  return (
    <div>
      <div>
        <Button onClick={() => navigate('new')}>Add drop</Button>
      </div>
      <div>Drops management</div>
      {drops.map((drop) => {
        return (
          <div key={drop.id}>
            <span>
              <b>{drop.name} </b> |{' '}
            </span>
            <span>{format(new Date(drop.startDateTime), appDateFormat)} | </span>
            <span>{format(new Date(drop.endDateTime), appDateFormat)}</span>
            <span>
              <Button onClick={() => navigate(drop.id.toString())}>Open</Button>
            </span>
          </div>
        );
      })}
    </div>
  );
};

export default DropsManagement;
