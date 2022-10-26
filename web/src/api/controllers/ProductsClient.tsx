import AxiosClient from '../Client';
import { AddProductRequest } from '../models/Products/AddProductRequest';
import { ProductDetailsDto } from '../models/Products/ProductDetailsDto';
import { ProductOnListDto } from '../models/Products/ProductOnListDto';

const controllerName = 'Products';

const getProducts = async (): Promise<ProductOnListDto[]> => {
  return (await AxiosClient.get(`${controllerName}`)).data;
};

const getProductById = async (productId: number): Promise<ProductDetailsDto> => {
  return (await AxiosClient.get(`${controllerName}/${productId}`)).data;
};

const addProduct = async (request: AddProductRequest): Promise<null> => {
  return (await AxiosClient.post(`${controllerName}`, request)).data;
};

// const getDropDetails = async (dropId: number): Promise<DropDetailsDto> => {
//   return (await AxiosClient.get(`${controllerName}/${dropId}`)).data;
// };

// const getDropsWithDetails = async (): Promise<DropDetailsDto[]> => {
//   return (await AxiosClient.get(`${controllerName}`)).data;
// };

export { getProducts, getProductById, addProduct };
