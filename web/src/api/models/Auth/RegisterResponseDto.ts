import { UserDto } from "../User/UserDto";

export interface RegisterResponseDto {
    user: UserDto;
    errors: string[];
}
