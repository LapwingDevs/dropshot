import AxiosClient from '../Client';
import { SubmitOrderRequest } from '../models/Orders/SubmitOrderRequest';

const controllerName = 'Orders';

const submitOrder = async (request: SubmitOrderRequest): Promise<number> => {
  return (await AxiosClient.post(`${controllerName}/submit`, request)).data;
};

const setOrderAsPaid = async (orderId: number): Promise<null> => {
  return (await AxiosClient.post(`${controllerName}/paid/${orderId}`)).data;
};

export { submitOrder, setOrderAsPaid };
