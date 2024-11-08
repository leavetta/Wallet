import { Outlet } from 'react-router-dom';

const ProtectedRoute = () => {
  const useAuth = () => {
    const user = localStorage.getItem('user');
    return user;
  };

  const navigate = () => {
    alert('You are unauthorised to access this. Please sign in');
    window.location="/login";
  }

  const isAuth = useAuth();
  return (
      isAuth ? (<Outlet /> ) : navigate()
  )
}

export default ProtectedRoute