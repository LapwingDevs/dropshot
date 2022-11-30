import { format } from 'date-fns';
import React, { useCallback, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { addDropItemToCart, getUserCart } from '../../api/controllers/CartsClient';
import { getDropDetails } from '../../api/controllers/DropsClient';
import { AddDropItemToUserCartRequest } from '../../api/models/Carts/AddDropItemToUserCartRequest';
import { DropDetailsDto } from '../../api/models/Drops/DropDetailsDto';
import { DropItemDto } from '../../api/models/Drops/DropItemDto';
import DropItemCard from '../../components/DropDetails/DropItemCard/DropItemCard';
import { appDateFormat } from '../../constants/Dates';
import { useCart } from '../../contexts/CartContext';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import data from '../../config.json';
import './DropDetails.scss';
import { Typography } from '@mui/material';

const DropDetails = () => {
  const [connection, setConnection] = useState<HubConnection | null>(null);

  const [drop, setDrop] = useState<DropDetailsDto | undefined>(undefined);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const { dropId } = useParams();
  const navigate = useNavigate();
  const { userCart, setUserCart } = useCart();

  const fetchDropDetails = useCallback(() => {
    if (dropId) {
      getDropDetails(+dropId).then((dropDetails) => {
        setDrop(dropDetails);
        setIsLoading(false);
      });
    }
  }, []);

  const addItemToUserCart = (dropItemId: number) => {
    if (userCart === undefined) {
      return;
    }

    const request: AddDropItemToUserCartRequest = {
      userCartId: userCart.id,
      dropItemId: dropItemId,
    };

    addDropItemToCart(request).then(() => {
      getUserCart().then((cart) => {
        setUserCart(cart);
      });
    });
  };

  useEffect(() => {
    if (!dropId) {
      return;
    }

    const newConnection = new HubConnectionBuilder()
      .withUrl(`${data.ApiUrl}DropsSocket`)
      .configureLogging(LogLevel.Debug)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, [dropId]);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          connection.invoke('JoinDropHub', dropId);

          connection.on('DropItemReserved', (dropItemId: number) => {
            fetchDropDetails();
          });

          connection.on('DropItemReleased', (dropItemId: number) => {
            fetchDropDetails();
          });

          connection.onreconnected(() => {
            connection.invoke('JoinDropHub', dropId);
          });

          console.log('Connection started.');
        })
        .catch((e) => {
          console.log(`SignalR connection on the client side failed.\nError content: ${e}`);
        });
    }
  }, [connection]);

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
          <Typography variant="h4" className="title">
            {drop.name}
          </Typography>
          <Typography variant="h6" className="dates">
            {format(new Date(drop.startDateTime), appDateFormat)} - {format(new Date(drop.endDateTime), appDateFormat)}
          </Typography>
          <Typography variant="h6" className="description">
            {drop.description}
          </Typography>

          <Typography variant="h6" sx={{ marginTop: '15px' }}>
            Available:
          </Typography>
          <div className="drop-items-wrapper">
            {drop.availableDropItems.map((dropItem) => {
              return (
                <DropItemCard
                  key={dropItem.dropItemId}
                  dropItem={dropItem}
                  addItemToUserCart={() => addItemToUserCart(dropItem.dropItemId)}
                />
              );
            })}
          </div>

          <Typography variant="h6" sx={{ marginTop: '15px' }}>
            Reserved:
          </Typography>
          <div className="drop-items-wrapper">
            {drop.reservedDropItems.map((dropItem) => {
              return (
                <DropItemCard
                  key={dropItem.dropItemId}
                  dropItem={dropItem}
                  addItemToUserCart={() => addItemToUserCart(dropItem.dropItemId)}
                  reserved={true}
                />
              );
            })}
          </div>
        </div>
      );
    }
  }
};

export default DropDetails;
