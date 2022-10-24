import AxiosClient from '../Client';
import { DropDetailsDto } from '../models/Drops/DropDetailsDto';
import { DropsLandingPageVm } from '../models/Drops/DropsLandingPageVm';

const controllerName = 'Drops';

const getDrops = async (): Promise<DropsLandingPageVm> => {
  return (await AxiosClient.get(`${controllerName}`)).data;
};

const getDropDetails = async (dropId: number): Promise<DropDetailsDto> => {
  return (await AxiosClient.get(`${controllerName}/${dropId}`)).data;
};

const getDropsWithDetails = async (): Promise<DropDetailsDto[]> => {
  return (await AxiosClient.get(`${controllerName}`)).data;
};

export { getDrops, getDropDetails, getDropsWithDetails };
