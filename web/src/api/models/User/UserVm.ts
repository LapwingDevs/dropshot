import { UserDto } from "./UserDto";

export interface UserVm {
    users: UserDto[];
    count: number;
}
