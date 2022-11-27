import { AddressDto } from './AddressDto';

export interface UpdateUserDto {
  id: number;
  firstName: string;
  lastName: string;
  address: AddressDto;
}
