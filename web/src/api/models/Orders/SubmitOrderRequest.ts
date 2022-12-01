import { CartItemDto } from '../Carts/CartItemDto';

export interface SubmitOrderRequest {
  totalPrice: number;
  shippingCost: number;
  cartItems: CartItemDto[];
}
