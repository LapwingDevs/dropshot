import { useEffect, useState } from 'react';
import { ping } from '../../api/controllers/MaintenanceClient';
import { useNavigate } from 'react-router-dom';
import './Root.scss';

const Root = () => {
  // GET FROM STATE
  const [isAuthenticated, setIsAuthenticated] = useState(true);

  const navigate = useNavigate();

  useEffect(() => {
    ping().then((res) => {
      console.log(res);
    });
  }, []);

  useEffect(() => {
    if (isAuthenticated) {
      navigate('/drops');
    }
  }, [isAuthenticated, navigate]);

  return <div>landing page</div>;
};

export default Root;
