import { AddressDto } from '../User/AddressDto';

export interface RegisterUserDto {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  address?: AddressDto;
}
