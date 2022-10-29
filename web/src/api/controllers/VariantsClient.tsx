import AxiosClient from '../Client';
import { AddVariantRequest } from '../models/Variants/AddVariantRequest';
import { VariantDto } from '../models/Variants/VariantDto';

const controllerName = 'Variants';

const addVariantToProduct = async (request: AddVariantRequest): Promise<null> => {
  return (await AxiosClient.post(`${controllerName}`, request)).data;
};

const getWarehouseVariants = async (): Promise<VariantDto[]> => {
  return (await AxiosClient.get(`${controllerName}/warehouse`)).data;
};

const removeVariant = async (variantId: number): Promise<null> => {
  return (await AxiosClient.delete(`${controllerName}/${variantId}`)).data;
};

export { addVariantToProduct, getWarehouseVariants, removeVariant };
