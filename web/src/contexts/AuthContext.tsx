import * as React from 'react';
import { UserDto } from '../api/models/User/UserDto';
import { login, logout } from '../api/controllers/AuthClient';
import { getUser } from '../api/controllers/UserClient';
import jwtDecode from 'jwt-decode';

export type AuthContextType = {
  currentUser?: UserDto;
  userRole?: string;
  signIn: (email: string, password: string) => void;
  signOut: () => void;
  isAuthLoading: boolean;
};

const AuthContext = React.createContext<AuthContextType>({
  currentUser: undefined,
  userRole: undefined,
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  signIn: () => {},
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  signOut: () => {},
  isAuthLoading: false,
});

export default AuthContext;

interface ProviderProps {
  children: React.ReactNode;
}

export const AuthProvider = ({ children }: ProviderProps) => {
  const [isAuthLoading, setIsAuthLoading] = React.useState(true);
  const [currentUser, setCurrentUser] = React.useState<UserDto>();
  const [userRole, setUserRole] = React.useState<string>();

  React.useEffect(() => {
    const token = sessionStorage.getItem('token');
    if (!currentUser && token != null) {
      getUser().then((userData) => {
        setCurrentUser(userData);
      });

      setUserRole(decodeJwt(token));
    }
  }, [currentUser]);

  const signIn = async (email: string, password: string) => {
    await login({ email: email, password: password }).then((userData) => {
      if (userData.errors.length > 0) {
        throw new Error(userData.errors[0]);
      }
      sessionStorage.setItem('token', userData.token);

      setCurrentUser(userData.user);
      setUserRole(decodeJwt(userData.token));
      setIsAuthLoading(false);
    });
  };

  const decodeJwt = (token: string) => {
    const decodedToken: never = jwtDecode(token);
    return decodedToken['role'];
  };

  const signOut = async () => {
    if (sessionStorage.getItem('token') != null) {
      if (currentUser) {
        await logout(currentUser.email).then((result) => {
          if (!result.succeeded) {
            throw new Error(result.errors[0]);
          }
        });
      }
    }

    sessionStorage.removeItem('token');
    setCurrentUser(undefined);
    setUserRole(undefined);
  };

  const stateValues = {
    currentUser,
    userRole,
    signIn,
    signOut,
    isAuthLoading,
  };

  return <AuthContext.Provider value={stateValues}>{children}</AuthContext.Provider>;
};
