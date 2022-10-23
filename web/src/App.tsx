import React from 'react';
import { RouterProvider } from 'react-router-dom';
import TopBar from './components/TopBar/TopBar';
import { router } from './routes/router';

const App = () => {
  // useAuth

  return (
    <div>
      <TopBar />
      {/* TODO: Consider using the classic approach of react router */}
      <RouterProvider router={router} />
    </div>
  );
};

export default App;
