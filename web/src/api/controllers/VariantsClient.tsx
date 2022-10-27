import AxiosClient from '../Client';
import { AddVariantRequest } from '../models/Variants/AddVariantRequest';

const controllerName = 'Variants';

const addVariantToProduct = async (request: AddVariantRequest): Promise<null> => {
  return (await AxiosClient.post(`${controllerName}`, request)).data;
};

export { addVariantToProduct };
