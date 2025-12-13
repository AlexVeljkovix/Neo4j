import React from "react";
import { Link } from "react-router-dom";

const LargeGameCard = ({ game }) => {
  return (
    <Link
      to={`/games/${game.id}`}
      className="block bg-gray-800 hover:bg-gray-700 rounded-lg shadow-md p-6 w-full transition-colors duration-200"
    >
      {/* Naslov igre */}
      <h2 className="text-2xl font-bold mb-2 text-white">{game.title}</h2>

      {/* Autor i izdavač */}
      {game.author && (
        <p className="text-gray-300 mb-1">
          <span className="font-semibold">Autor:</span> {game.author.firstName}{" "}
          {game.author.lastName} ({game.author.country})
        </p>
      )}
      {game.publisher && (
        <p className="text-gray-300 mb-1">
          <span className="font-semibold">Izdavač:</span> {game.publisher.name}{" "}
          ({game.publisher.country})
        </p>
      )}

      {/* Težina i dostupne jedinice */}
      <p className="text-gray-300 mb-1">
        <span className="font-semibold">Težina:</span> {game.difficulty}
      </p>
      <p className="text-gray-300 mb-3">
        <span className="font-semibold">Dostupno:</span> {game.availableUnits}
      </p>

      {/* Deo deskripcije */}
      {game.description && (
        <p className="text-gray-400 line-clamp-3">{game.description}</p>
      )}
    </Link>
  );
};

export default LargeGameCard;
