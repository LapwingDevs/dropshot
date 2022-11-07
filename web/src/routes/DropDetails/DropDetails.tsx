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
import './DropDetails.scss';

const DropDetails = () => {
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
              return (
                <DropItemCard
                  key={dropItem.dropItemId}
                  dropItem={dropItem}
                  addItemToUserCart={() => addItemToUserCart(dropItem.dropItemId)}
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
