import { format } from 'date-fns';
import React, { useCallback, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getDropDetails } from '../../api/controllers/DropsClient';
import { DropDetailsDto } from '../../api/models/Drops/DropDetailsDto';
import DropItemCard from '../../components/DropDetails/DropItemCard/DropItemCard';
import { appDateFormat } from '../../constants/Dates';
import './DropDetails.scss';

const DropDetails = () => {
  const [drop, setDrop] = useState<DropDetailsDto | undefined>(undefined);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const { dropId } = useParams();
  const navigate = useNavigate();

  const fetchDropDetails = useCallback(() => {
    if (dropId) {
      getDropDetails(+dropId).then((dropDetails) => {
        console.log(dropDetails);
        setDrop(dropDetails);
        setIsLoading(false);
      });
    }
  }, []);

  useEffect(() => {
    fetchDropDetails();
  }, []);

  if (isLoading === true) {
    return <div>loading..</div>;
  } else {
    if (drop === undefined) {
      navigate('/');
      return <div />;
    } else {
      return (
        <div className="drop-container">
          <div className="title">{drop.name}</div>
          <div className="dates">
            {format(new Date(drop.startDateTime), appDateFormat)} - {format(new Date(drop.endDateTime), appDateFormat)}
          </div>
          <div className="description">{drop.description}</div>

          <div className="drop-items-wrapper">
            {drop.dropItems.map((dropItem) => {
              return <DropItemCard key={dropItem.id} dropItem={dropItem} />;
            })}
          </div>
        </div>
      );
    }
  }
};

export default DropDetails;
