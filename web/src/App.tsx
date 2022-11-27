import React, { useContext } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import TopBar from './components/Common/TopBar/TopBar';
import Account from './routes/Account/Account';
import AdminPanel from './routes/AdminPanel/AdminPanel';
import AddNewDrop from './routes/AdminPanel/DropsManagement/AddNewDrop/AddNewDrop';
import DropsManagement from './routes/AdminPanel/DropsManagement/DropsManagement';
import ManageDropPage from './routes/AdminPanel/DropsManagement/ManageDropPage/ManageDropPage';
import AddNewProduct from './routes/AdminPanel/ProductsManagement/AddNewProduct/AddNewProduct';
import Product from './routes/AdminPanel/ProductsManagement/ProductPage/Product';
import ProductsManagement from './routes/AdminPanel/ProductsManagement/ProductsManagement';
import UsersManagement from './routes/AdminPanel/UsersManagement/UsersManagement';
import DropDetails from './routes/DropDetails/DropDetails';
import Drops from './routes/Drops/Drops';
import ErrorPage from './routes/ErrorPage/ErrorPage';
import Login from './routes/Login/Login';
import Order from './routes/Order/Order';
import Register from './routes/Register/Register';
import Root from './routes/Root/Root';
import AuthContext, { AuthProvider } from './contexts/AuthContext';
import PrivateRoute from './routes/Auth/PrivateRoute';
import AdminRoute from './routes/Auth/AdminRoute';
import AddAdmin from './routes/AdminPanel/UsersManagement/AddAdmin/AddAdmin';
import AdminList from './routes/AdminPanel/UsersManagement/AdminList/AdminList';
import { SnackbarProvider } from 'notistack';
import AxiosClient from './api/Client';
import { refresh } from './api/controllers/AuthClient';
import UnauthorizedRoute from './routes/Auth/UnauthorizedRoute';

const App = () => {
  const { isAuthLoading, currentUser } = useContext(AuthContext);

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
  };

  React.useEffect(() => {
    if (!isAuthLoading) {
      getAccessTokenAndSetAxiosInterceptors().then(() => {
        console.log('DU{A');
      });
    }
  }, [isAuthLoading]);

  return (
    <BrowserRouter>
      <AuthProvider>
        <SnackbarProvider
          autoHideDuration={1500}
          maxSnack={2}
          anchorOrigin={{ horizontal: 'center', vertical: 'bottom' }}
        >
          <TopBar />
          <Routes>
            <Route path="/" element={<Root />} />
            <Route element={<UnauthorizedRoute />}>
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
            </Route>
            {/* Authenticated paths */}
            <Route element={<PrivateRoute />}>
              <Route path="/drops" element={<Drops />} />
              <Route path="/drops/:dropId" element={<DropDetails />} />
              <Route path="/account" element={<Account />} />
              <Route path="/order" element={<Order />} />
            </Route>
            <Route element={<AdminRoute />}>
              <Route path="/admin-panel" element={<AdminPanel />} />
              <Route path="/admin-panel/users-management" element={<UsersManagement />} />
              <Route path="/admin-panel/users-management/add-admin" element={<AddAdmin />} />
              <Route path="/admin-panel/users-management/admin-list" element={<AdminList />} />
              <Route path="/admin-panel/drops-management" element={<DropsManagement />} />
              <Route path="/admin-panel/drops-management/:dropId" element={<ManageDropPage />} />
              <Route path="/admin-panel/drops-management/new" element={<AddNewDrop />} />
              <Route path="/admin-panel/products-management" element={<ProductsManagement />} />
              <Route path="/admin-panel/products-management/:productId" element={<Product />} />
              <Route path="/admin-panel/products-management/new" element={<AddNewProduct />} />
            </Route>
            {/*  */}
            <Route path="*" element={<ErrorPage />} />
          </Routes>
        </SnackbarProvider>
      </AuthProvider>
    </BrowserRouter>
  );
};

export default App;
