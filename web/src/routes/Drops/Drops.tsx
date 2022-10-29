import React from 'react';
import { useCallback, useEffect, useState } from 'react';
import { getDrops } from '../../api/controllers/DropsClient';
import { DropCardDto } from '../../api/models/Drops/DropCardDto';
import DropCard from '../../components/Drops/DropCard/DropCard';
import { DropStatus } from '../../constants/Drops';
import './Drops.scss';

const Drops = () => {
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [activeDrops, setActiveDrops] = useState<DropCardDto[]>([]);
  const [incomingDrops, setIncomingDrops] = useState<DropCardDto[]>([]);

  const fetchDrops = useCallback(async () => {
    const drops = await getDrops();
    setActiveDrops(drops.activeDrops);
    setIncomingDrops(drops.incomingDrops);

    setIsLoading(false);
  }, []);

  useEffect(() => {
    fetchDrops();
  }, [fetchDrops]);

  if (isLoading) {
    return <div>loading..</div>;
  }

  return (
    <div className="drops-container">
      <div className="section-title">active</div>
      <div className="section-content">
        {activeDrops.map((drop) => {
          return <DropCard key={drop.id} drop={drop} dropStatus={DropStatus.ActiveDrop} />;
        })}
      </div>
      <div className="section-title">incoming</div>
      <div className="section-content">
        {incomingDrops.map((drop) => {
          return <DropCard key={drop.id} drop={drop} dropStatus={DropStatus.IncomingDrop} />;
        })}
      </div>
    </div>
  );
};

export default Drops;
