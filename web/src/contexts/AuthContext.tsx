import * as React from 'react';
import { UserDto } from '../api/models/User/UserDto';
import { login, logout, refresh } from '../api/controllers/AuthClient';
import { getUser } from '../api/controllers/UserClient';
import jwtDecode from 'jwt-decode';
import AxiosClient from '../api/Client';

export type AuthContextType = {
  currentUser?: UserDto;
  userRole?: string;
  signIn: (email: string, password: string) => void;
  signOut: () => void;
  isAuthenticated: () => boolean;
};

const AuthContext = React.createContext<AuthContextType>({
  currentUser: undefined,
  userRole: undefined,
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  signIn: () => {},
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  signOut: () => {},
  isAuthenticated: () => false,
});

export default AuthContext;

interface ProviderProps {
  children: React.ReactNode;
}

export const AuthProvider = ({ children }: ProviderProps) => {
  const [isAuthLoading, setIsAuthLoading] = React.useState(true);
  const [currentUser, setCurrentUser] = React.useState<UserDto>();
  const [userRole, setUserRole] = React.useState<string>();

  const getAccessTokenAndSetAxiosInterceptors = async () => {
    const accessToken = sessionStorage.getItem('token');
    if (accessToken !== null) {
      setAxiosRequestInterceptor();
      setAxiosResponseInterceptor();
    }
  };

  const setAxiosRequestInterceptor = () => {
    AxiosClient.interceptors.request.use(
      (config) => {
        if (config && config.headers) {
          const accessToken = sessionStorage.getItem('token');
          config.headers['Authorization'] = `Bearer ${accessToken}`;
        }
        return config;
      },
      () => {
        alert('Incorporating token went wrong!');
      },
    );
  };

  const setAxiosResponseInterceptor = () => {
    AxiosClient.interceptors.response.use(
      (response) => response,
      async (error) => {
        const originalConfig = error.config;

        if (error.response.status === 401 && !originalConfig.send) {
          originalConfig.send = true;

          try {
            await refresh().then((response) => {
              if (!response.succeeded) {
                throw Error(response.errors[0]);
              }

              sessionStorage.setItem('token', response.token);
              originalConfig.headers['Authorization'] = `Bearer ${response.token}`;

              return AxiosClient(originalConfig);
            });
          } catch (_error) {
            return Promise.reject(_error);
          }
        }
      },
    );

    //AxiosClient.interceptors.request.eject(requestIntercept);
    //AxiosClient.interceptors.response.eject(responseIntercept);
  };

  React.useEffect(() => {
    if (!isAuthLoading) {
      getAccessTokenAndSetAxiosInterceptors().then(() => {
        console.log('DU{A');
      });
    }
  }, [isAuthLoading]);

  /*  React.useEffect(() => {
    console.log(currentUser);
    console.log(userRole);
    console.log(isAuthenticated());
  }, [currentUser, userRole]);*/

  React.useEffect(() => {
    const token = sessionStorage.getItem('token');
    if (!currentUser && token != null) {
      getUser().then((userData) => {
        setCurrentUser(userData);
      });

      setUserRole(decodeJwt(token));
    }
  }, [currentUser]);

  const isAuthenticated = () => {
    return currentUser !== undefined;
  };

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
    isAuthenticated,
  };

  return <AuthContext.Provider value={stateValues}>{children}</AuthContext.Provider>;
};
