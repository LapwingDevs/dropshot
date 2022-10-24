import React from 'react';
import { BrowserRouter, Route, RouterProvider, Routes } from 'react-router-dom';
import TopBar from './components/TopBar/TopBar';
import Account from './routes/Account/Account';
import AdminPanel from './routes/AdminPanel/AdminPanel';
import DropsManagement from './routes/AdminPanel/DropsManagement/DropsManagement';
import UsersManagement from './routes/AdminPanel/UsersManagement/UsersManagement';
import DropDetails from './routes/DropDetails/DropDetails';
import Drops from './routes/Drops/Drops';
import ErrorPage from './routes/ErrorPage/ErrorPage';
import Login from './routes/Login/Login';
import Register from './routes/Register/Register';
import Root from './routes/Root/Root';

const App = () => {
  // useAuth

  return (
    <BrowserRouter>
      <TopBar />
      <Routes>
        <Route path="/" element={<Root />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        {/* Authenticated paths */}
        <Route path="/drops" element={<Drops />} />
        <Route path="/drop/:id" element={<DropDetails />} />
        <Route path="/admin-panel" element={<AdminPanel />} />
        <Route path="/admin-panel/users-management" element={<UsersManagement />} />
        <Route path="/admin-panel/drops-management" element={<DropsManagement />} />
        <Route path="/account" element={<Account />} />
        {/*  */}
        <Route path="*" element={<ErrorPage />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
