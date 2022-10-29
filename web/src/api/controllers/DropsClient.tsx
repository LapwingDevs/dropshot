import AxiosClient from '../Client';
import { AddDropRequest } from '../models/Drops/AddDropRequest';
import { AdminDropDto } from '../models/Drops/AdminDropDto';
import { DropDetailsDto } from '../models/Drops/DropDetailsDto';
import { DropsLandingPageVm } from '../models/Drops/DropsLandingPageVm';

const controllerName = 'Drops';

const getDrops = async (): Promise<DropsLandingPageVm> => {
  return (await AxiosClient.get(`${controllerName}`)).data;
};

const getDropDetails = async (dropId: number): Promise<DropDetailsDto> => {
  return (await AxiosClient.get(`${controllerName}/${dropId}`)).data;
};

const getDropsWithDetails = async (): Promise<AdminDropDto[]> => {
  return (await AxiosClient.get(`${controllerName}/admin`)).data;
};

const addDrop = async (request: AddDropRequest): Promise<null> => {
  return (await AxiosClient.post(`${controllerName}`, request)).data;
};

export { getDrops, getDropDetails, getDropsWithDetails, addDrop };
