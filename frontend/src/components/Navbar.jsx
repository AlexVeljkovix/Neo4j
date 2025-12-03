import React, { useState } from "react";
import logo from "../assets/logo.png";

const Navbar = () => {
  const [searchType, setSearchType] = useState("Games");

  return (
    <header className="bg-primary py-3 px-6 shadow-md">
      <div className="flex items-center justify-between">
        {/* Levo */}
        <div className="flex items-center gap-6">
          <img src={logo} alt="Logo" className="w-12" />
          <div className="text-2xl text-text font-sans font-bold">
            Boardgame Cafe
          </div>
        </div>

        {/* Desno */}
        <div className="flex items-stretch gap-0">
          <input
            type="text"
            placeholder="Search"
            className="
      w-32 md:w-52
      transition-all duration-300
      hover:w-44 md:hover:w-72
      focus:w-44 md:focus:w-72
      px-3 py-2
      text-text font-bold
      border-2 border-secondary border-r-0
      rounded-l-full
      bg-input
      focus:outline-none 
      placeholder-secondary
      box-border
    "
          />
          <select
            value={searchType}
            onChange={(e) => setSearchType(e.target.value)}
            className="
      px-3 py-2
      text-text font-bold
      bg-input
      border-2 border-secondary border-l-0
      rounded-r-full
      focus:outline-none 
      cursor-pointer
      box-border
    "
          >
            <option value="Games">Games</option>
            <option value="Authors">Authors</option>
            <option value="Publishers">Publishers</option>
          </select>
        </div>
      </div>
    </header>
  );
};

export default Navbar;
