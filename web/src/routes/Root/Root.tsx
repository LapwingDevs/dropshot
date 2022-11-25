import React from 'react';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Root.scss';
import AuthContext from '../../contexts/AuthContext';

const Root = () => {
  // GET FROM STATE
  const { currentUser } = React.useContext(AuthContext);

  const navigate = useNavigate();

  useEffect(() => {
    if (sessionStorage.getItem('token') === null) {
      navigate('/drops');
    } else {
      navigate('/login');
    }
  }, [navigate]);

  return <div>landing page</div>;
};

export default Root;
