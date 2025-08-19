import { Outlet, useLocation } from 'react-router-dom';
import './App.css'
import MovieTable from './components/movies/MovieTable'
import { useEffect } from 'react';
import { serUpErrorHandlerInterceptor } from './interceptors/axiosInterceptor';

function App() {
  const location = useLocation();

  useEffect(() => {
    serUpErrorHandlerInterceptor();
  }, []);

  return (
    <>
      {location.pathname === '/' ? <MovieTable /> : (
        <div className="container">
          <Outlet />
        </div>
      )}
    </>
  )
}

export default App
