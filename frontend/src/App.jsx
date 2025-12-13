import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import Navbar from "./components/Navbar.jsx";
import Footer from "./components/Footer.jsx";
import GamesPage from "./pages/GamesPage.jsx";
import AuthorsPage from "./pages/AuthorsPage.jsx";
import PublishersPage from "./pages/PublishersPage.jsx";
import RentalsPage from "./pages/RentalsPage.jsx";
import GameDetailsPage from "./pages/GameDetailsPage.jsx";
import AuthorDetailsPage from "./pages/AuthorDetailsPage.jsx";
import PublisherDetailsPage from "./pages/PublisherDetailsPage.jsx";
import RentalDetailsPage from "./pages/RentalDetailsPage.jsx";

import { PublisherProvider } from "./context/PublisherContext";
import { AuthorProvider } from "./context/AuthorContext.jsx";
import { GameProvider } from "./context/GameContext.jsx";
import { RentalProvider } from "./context/RentalContext.jsx";
import { SearchProvider } from "./context/SearchContext.jsx";
const App = () => {
  return (
    <BrowserRouter>
      <SearchProvider>
        <RentalProvider>
          <GameProvider>
            <AuthorProvider>
              <PublisherProvider>
                <div className="min-h-screen flex flex-col">
                  <Navbar />

                  <div className="grow">
                    <Routes>
                      <Route
                        path="/"
                        element={<Navigate to="/games" replace />}
                      />
                      <Route path="/games" element={<GamesPage />} />
                      <Route path="/games/:id" element={<GameDetailsPage />} />
                      <Route path="/authors" element={<AuthorsPage />} />
                      <Route
                        path="/authors/:id"
                        element={<AuthorDetailsPage />}
                      />
                      <Route path="/publishers" element={<PublishersPage />} />
                      <Route
                        path="/publishers/:id"
                        element={<PublisherDetailsPage />}
                      />
                      <Route path="/rentals" element={<RentalsPage />} />
                      <Route
                        path="/rentals/:id"
                        element={<RentalDetailsPage />}
                      />
                    </Routes>
                  </div>

                  <Footer />
                </div>
              </PublisherProvider>
            </AuthorProvider>
          </GameProvider>
        </RentalProvider>
      </SearchProvider>
    </BrowserRouter>
  );
};

export default App;
