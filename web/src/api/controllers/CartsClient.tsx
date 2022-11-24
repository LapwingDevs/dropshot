import AxiosClient from '../Client';
import { AddDropItemToUserCartRequest } from '../models/Carts/AddDropItemToUserCartRequest';
import { UserCartDto } from '../models/Carts/UserCartDto';

const controllerName = 'Carts';

const getUserCart = async (): Promise<UserCartDto> => {
  return (await AxiosClient.get(`${controllerName}`)).data;
};

const addDropItemToCart = async (request: AddDropItemToUserCartRequest): Promise<null> => {
  return (await AxiosClient.post(`${controllerName}`, request)).data;
};

export { addDropItemToCart, getUserCart };
