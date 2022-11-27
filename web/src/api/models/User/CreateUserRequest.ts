import { AddressDto } from "./AddressDto";

export interface CreateUserRequest {
    firstName: string;
    lastName: string;
    email: string;
    applicationUserId: string;
    address: AddressDto;
}
