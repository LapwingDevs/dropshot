import { UserDto } from "../User/UserDto";

export interface LoginUserResponse {
    token: string;
    refreshToken: string;
    user: UserDto;
    errors: string[];
}
