import AxiosClient from "../Client";

const controllerName = "Maintenance";

const ping = async (): Promise<string> => {
  return (await AxiosClient.get(`${controllerName}/ping`)).data;
};

export { ping };
