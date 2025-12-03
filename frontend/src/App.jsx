import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import Navbar from "./components/Navbar.jsx";
import Footer from "./components/Footer.jsx";
import NavbarTW from "./components/NavbarTW.jsx";
import FooterTW from "./components/FooterTW.jsx";
import HomePage from "./pages/HomePage.jsx";

const App = () => {
  return (
    <BrowserRouter>
      {/* Glavni layout mora biti flex kolona i full height */}
      <div className="min-h-screen flex flex-col">
        {/*<Navbar />*/}
        <NavbarTW />

        {/* Sadržaj treba da se rastegne da gura footer na dno */}
        <div className="grow">
          <Routes>
            <Route path="/" element={<HomePage />} />
          </Routes>
        </div>
        <FooterTW />
        {/*<Footer />*/}
      </div>
    </BrowserRouter>
  );
};

export default App;
