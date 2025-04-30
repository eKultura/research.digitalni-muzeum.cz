// Layout.jsx
import Navbar from './Navbar';
import { Outlet } from 'react-router';

function Layout() {
  return (
    <>
      <Navbar />
      <div className="flex flex-col gap-2 mt-15 items-center text-center">
      <div className="text-3xl font-large font-bold mb-3">Vyhledávání souvislostí v PDF dokumentech</div>
      </div>
      <main>
        <Outlet />
      </main>
    </>
  );
}

export default Layout;