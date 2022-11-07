import React from 'react';
import { UserCartDto } from '../api/models/Carts/UserCartDto';

interface ICartContext {
  userCart: UserCartDto | undefined;
  setUserCart: (cartItems: UserCartDto) => void;
}

const defaultState: ICartContext = {
  userCart: undefined,
  setUserCart: () => {
    return;
  },
};

const CartContext = React.createContext<ICartContext>(defaultState);

type Props = {
  children: React.ReactNode;
};

export const CartProvider = ({ children }: Props) => {
  const [userCart, setUserCart] = React.useState<UserCartDto | undefined>(undefined);

  const value: ICartContext = {
    userCart: userCart,
    setUserCart: setUserCart,
  };

  return <CartContext.Provider value={value}>{children}</CartContext.Provider>;
};

export const useCart = () => {
  const context = React.useContext(CartContext);
  if (context === undefined) {
    throw new Error('useCart must be used within a RedirectProvider');
  }
  return context;
};
