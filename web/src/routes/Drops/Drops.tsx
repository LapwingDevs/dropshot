import { useCallback, useEffect, useState } from "react";
import { getDrops } from "../../api/controllers/DropsClient";
import { DropCardDto } from "../../api/models/Drops/DropCardDto";
import DropCard from "../../components/Drops/DropCard/DropCard";
import "./Drops.scss";

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
    <div>
      <div>active</div>
      <div>
        {activeDrops.map((drop) => {
          return <DropCard key={drop.id} drop={drop} />;
        })}
      </div>
      <div>incoming</div>
      <div>
        {incomingDrops.map((drop) => {
          return <DropCard key={drop.id} drop={drop} />;
        })}
      </div>
    </div>
  );
};

export default Drops;
