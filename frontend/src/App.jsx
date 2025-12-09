import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import Navbar from "./components/Navbar.jsx";
import Footer from "./components/Footer.jsx";
import GamesPage from "./pages/GamesPage.jsx";
import AuthorsPage from "./pages/AuthorsPage.jsx";
import PublishersPage from "./pages/PublishersPage.jsx";
import RentalsPage from "./pages/RentalsPage.jsx";

const App = () => {
  return (
    <BrowserRouter>
      {/* Glavni layout mora biti flex kolona i full height */}
      <div className="min-h-screen flex flex-col">
        {/*<Navbar />*/}
        <Navbar />

        {/* Sadržaj treba da se rastegne da gura footer na dno */}
        <div className="grow">
          <Routes>
            <Route path="/" element={<Navigate to="/games" replace />} />
            <Route path="/games" element={<GamesPage />} />
            <Route path="/authors" element={<AuthorsPage />} />
            <Route path="/publishers" element={<PublishersPage />} />
            <Route path="/rentals" element={<RentalsPage />} />
          </Routes>
        </div>
        <Footer />
        {/*<Footer />*/}
      </div>
    </BrowserRouter>
  );
};

export default App;
