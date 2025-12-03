import { CheckIcon } from "@heroicons/react/20/solid";
import { useEffect, useState } from "react";
import CardTW from "../components/CardTW";
import { getGames } from "../api/gameApi";

export default function HomePage() {
  const [games, setGames] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    getGames()
      .then(setGames)
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  return (
    <div className="mx-6 my-4">
      {/* Naslov i dugme */}
      <div className="flex flex-col sm:flex-row sm:justify-between sm:items-center mb-6">
        <h2 className="text-2xl font-bold sm:text-3xl sm:tracking-tight mb-4 sm:mb-0">
          Sve dostupne igre
        </h2>
        <button
          type="button"
          className="inline-flex items-center rounded-md bg-gray-800 hover:cursor-pointer px-4 py-2 text-sm font-semibold text-white hover:bg-gray-700 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-900"
        >
          <CheckIcon aria-hidden="true" className="mr-1.5 h-5 w-5" />
          Dodaj novu igru
        </button>
      </div>

      {/* Status */}
      {loading && <p className="text-gray-400">Učitavanje...</p>}
      {error && <p className="text-red-500">Greška: {error}</p>}

      {/* Kartice */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {games.map((game) => (
          <div
            key={game.id}
            className="bg-gray-800 rounded-lg shadow-md p-4 text-white hover:bg-gray-700 transition"
          >
            <CardTW game={game} />
          </div>
        ))}
      </div>
    </div>
  );
}
