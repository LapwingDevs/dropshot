import { Button } from '@mui/material';
import React, { useCallback, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getDropsWithDetails } from '../../../api/controllers/DropsClient';
import { DropDetailsDto } from '../../../api/models/Drops/DropDetailsDto';

const DropsManagement = () => {
  const [drops, setDrops] = useState<DropDetailsDto[]>([]);
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
            {drop.name}, {drop.startDateTime}, {drop.endDateTime}
          </div>
        );
      })}
    </div>
  );
};

export default DropsManagement;
