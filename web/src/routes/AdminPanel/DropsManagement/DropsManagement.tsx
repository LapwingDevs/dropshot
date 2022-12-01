import { Button, Typography } from '@mui/material';
import { format } from 'date-fns';
import React, { useCallback, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getDropsWithDetails } from '../../../api/controllers/DropsClient';
import { AdminDropDto } from '../../../api/models/Drops/AdminDropDto';
import { appDateFormat } from '../../../constants/Dates';
import './DropsManagment.scss';

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
    <div className="container">
      <Typography variant="h4" sx={{ marginBottom: '10px' }}>
        Drops management
      </Typography>
      {drops.map((drop) => {
        return (
          <div className="item" key={drop.id}>
            <Typography variant="h6">
              <b>{drop.name} </b> |{' '}
            </Typography>
            <Typography variant="h6">{format(new Date(drop.startDateTime), appDateFormat)} | </Typography>
            <Typography variant="h6">{format(new Date(drop.endDateTime), appDateFormat)}</Typography>
{/*            <Button sx={{ marginLeft: '10px' }} onClick={() => navigate(drop.id.toString())}>
              Open
            </Button>*/}
          </div>
        );
      })}

      <div>
        <Button onClick={() => navigate('new')}>Add drop</Button>
      </div>
    </div>
  );
};

export default DropsManagement;
