import { RouterProvider } from 'react-router-dom';
import TopBar from './components/TopBar/TopBar';
import { router } from './routes/router';

const App = () => {
  // useAuth

  return (
    <div>
      <TopBar />
      <RouterProvider router={router} />
    </div>
  );
};

export default App;
