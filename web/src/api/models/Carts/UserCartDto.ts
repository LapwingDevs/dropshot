import { CartItemDto } from "./CartItemDto";

export interface UserCartDto {
    id: number;
    cartItems: CartItemDto[];
}