import React from "react";

const CardTW = ({ game }) => {
  return (
    <div className="bg-neutral-900/10 block  p-6 border border-gray-700 rounded-lg shadow-md">
      {/* Slika igre, ako postoji */}
      {game.ImageUrl && (
        <img
          src={game.ImageUrl}
          alt={game.Title}
          className="w-full h-40 object-cover rounded-md mb-4"
        />
      )}

      {/* Naslov igre */}
      <h5 className="mb-2 text-xl font-semibold tracking-tight text-white">
        {game.title}
      </h5>

      {/* Opis igre */}
      <p className="text-gray-300 mb-2">
        {game.description || "Nema opisa za ovu igru."}
      </p>

      {/* Autor i Publisher */}
      {game.author && (
        <p className="text-gray-400 text-sm mb-1">
          Autor: {game.author.firstName} {game.author.lastName} (
          {game.author.country})
        </p>
      )}
      {game.publisher && (
        <p className="text-gray-400 text-sm mb-3">
          Publisher: {game.publisher.name}
        </p>
      )}

      {/* Dugme za detalje */}
      <a
        href={`/games/${game.Id}`} // ili gde vodi detaljni prikaz
        className="inline-flex items-center text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:ring-blue-500 shadow font-medium rounded-lg text-sm px-4 py-2.5 focus:outline-none"
      >
        Pogledaj detalje
        <svg
          className="w-4 h-4 ms-1.5 rtl:rotate-180 -me-0.5"
          aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
        >
          <path
            stroke="currentColor"
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth="2"
            d="M19 12H5m14 0-4 4m4-4-4-4"
          />
        </svg>
      </a>
    </div>
  );
};

export default CardTW;
