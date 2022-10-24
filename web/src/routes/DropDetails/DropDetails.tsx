import React, { useCallback, useEffect } from 'react';
import { useParams } from 'react-router-dom';

interface IDropDetailsParams {
  id: string;
}

const DropDetails = () => {
  const { id } = useParams();

  //   const fetchDropDetails = useCallback(() => {}, []);

  useEffect(() => {
    console.log(id);
  }, []);
  return <div>drop details</div>;
};

export default DropDetails;
