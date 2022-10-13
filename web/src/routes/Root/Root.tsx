import { useEffect } from "react";
import { ping } from "../../api/controllers/MaintenanceClient";
import "./Root.scss";

const Root = () => {
  useEffect(() => {
    ping().then((res) => {
      console.log(res);
    });
  }, []);

  return <div>root</div>;
};

export default Root;
