import AxiosClient from "../Client";
import { DropsLandingPageVm } from "../models/Drops/DropsLandingPageVm";

const controllerName = "Drops";

const getDrops = async (): Promise<DropsLandingPageVm> => {
  return (await AxiosClient.get(`${controllerName}`)).data;
};

export { getDrops };
