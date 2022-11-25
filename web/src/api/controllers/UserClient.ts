import AxiosClient from '../Client';
import { UserDto } from '../models/User/UserDto';
import { UserVm } from '../models/User/UserVm';
import { UpdateUserDto } from '../models/User/UpdateUserDto';

const controllerName = 'User';

const getUser = async (): Promise<UserDto> => {
  return (await AxiosClient.get(`${controllerName}/loggedUser`)).data;
};

const updateUser = async (updateDto: UpdateUserDto): Promise<UserDto> => {
  return (await AxiosClient.put(`${controllerName}`, updateDto)).data;
};

const getUsers = async (term: string): Promise<UserVm> => {
  return (await AxiosClient.get(`${controllerName}/users`, { params: { term: term, usersOnly: true } })).data;
};

export { getUser, updateUser, getUsers };
