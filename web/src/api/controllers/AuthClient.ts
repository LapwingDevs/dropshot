import AxiosClient from '../Client';
import { noAuthClient } from '../Client';
import { LoginUserResponse } from '../models/Auth/LoginUserResponse';
import { LoginUserRequest } from '../models/Auth/LoginUserRequest';
import { JWTAuthorizationResult } from '../models/Auth/JWTAuthorizationResult';
import { Result } from '../models/Auth/Result';
import { UserDto } from '../models/User/UserDto';
import { RegisterUserDto } from '../models/Auth/RegisterUserDto';
import { RegisterResponseDto } from '../models/Auth/RegisterResponseDto';

const controllerName = 'Auth';

const login = async (request: LoginUserRequest): Promise<LoginUserResponse> => {
  return (await noAuthClient.post(`${controllerName}/login`, request)).data;
};

const register = async (request: RegisterUserDto): Promise<RegisterResponseDto> => {
  return (await noAuthClient.post(`${controllerName}/register`, request)).data;
};

const refresh = async (): Promise<JWTAuthorizationResult> => {
  return (await noAuthClient.post(`${controllerName}/refreshToken`)).data;
};

const logout = async (email: string): Promise<Result> => {
  return (await AxiosClient.post(`${controllerName}/logout/${email}`)).data;
};

const getAdmins = async (): Promise<UserDto[]> => {
  return (await AxiosClient.get(`${controllerName}/admins`)).data;
};

const degradeUser = async (email: string): Promise<any> => {
  return await AxiosClient.post(`${controllerName}/degrade/${email}`);
};

const promoteUser = async (email: string): Promise<any> => {
  return await AxiosClient.post(`${controllerName}/promote/${email}`);
};

export { login, register, logout, refresh, getAdmins, degradeUser, promoteUser };
