import {AddressDto} from "./AddressDto";

export interface UserDto{
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    address: AddressDto;
}
