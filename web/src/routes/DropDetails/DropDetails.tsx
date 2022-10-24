import React, { useCallback, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getDropDetails } from '../../api/controllers/DropsClient';
import { DropDetailsDto } from '../../api/models/Drops/DropDetailsDto';

const DropDetails = () => {
  const [drop, setDrop] = useState<DropDetailsDto | undefined>(undefined);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const { id } = useParams();

  const fetchDropDetails = useCallback(() => {
    if (id) {
      getDropDetails(+id).then((dropDetails) => {
        setDrop(dropDetails);
        setIsLoading(false);
      });
    }
  }, []);

  useEffect(() => {
    fetchDropDetails();
  }, []);

  if (isLoading) {
    return <div>loading..</div>;
  }
  return (
    <div>
      drop details {drop?.name}, {drop?.description}
    </div>
  );
};

export default DropDetails;
