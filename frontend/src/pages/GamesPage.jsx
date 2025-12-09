import { PlusIcon } from "@heroicons/react/20/solid";
import { ExclamationTriangleIcon } from "@heroicons/react/24/outline";
import { useEffect, useState } from "react";
import GamesList from "../components/Games/GamesList";
import { getGames } from "../api/gameApi";
import CreateGameForm from "../components//Games/CreateGameForm";

export default function GamesPage() {
  const [games, setGames] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);

  useEffect(() => {
    setTimeout(() => {
      getGames()
        .then((data) => {
          setGames(data);
          setLoading(false);
        })
        .catch((err) => {
          setError(err.message);
        })
        .finally(() => {
          setLoading(false);
        });
    }, 1000);
  }, []);

  return (
    <div className="mx-6 my-4 min-h-[80vh]">
      {/* Naslov i dugme */}
      <div className="flex flex-col sm:flex-row sm:justify-between sm:items-center mb-6">
        <h2 className="text-2xl font-bold sm:text-3xl sm:tracking-tight mb-4 sm:mb-0">
          Sve dostupne igre
        </h2>
        <button
          onClick={() => setShowForm(true)}
          type="button"
          className="inline-flex items-center rounded-md bg-gray-800 hover:cursor-pointer px-4 py-2 text-sm font-semibold text-white hover:bg-gray-700 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-900"
        >
          <PlusIcon aria-hidden="true" className="mr-1.5 h-5 w-5" />
          Dodaj novu igru
        </button>
      </div>

      {/* LOADING - SKELETON */}
      {loading && (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 animate-pulse">
          {Array.from({ length: 6 }).map((_, i) => (
            <div key={i} className="bg-gray-800 rounded-lg shadow-md p-4">
              <div className="bg-neutral-900/10 p-6 border border-gray-700 rounded-lg shadow-md h-60 flex flex-col">
                <div className="w-full h-40 bg-gray-700/40 rounded-md mb-4"></div>
                <div className="h-4 w-2/3 bg-gray-700/40 rounded mb-3"></div>
                <div className="h-3 w-1/2 bg-gray-700/40 rounded mb-2"></div>
                <div className="h-3 w-1/3 bg-gray-700/40 rounded mb-4"></div>
                <div className="mt-auto h-9 w-32 bg-gray-700/40 rounded"></div>
              </div>
            </div>
          ))}
        </div>
      )}

      {/* ERROR */}
      {!loading && error && (
        <div className="flex items-start gap-3 bg-red-900/20 border border-red-700 text-red-300 px-4 py-3 rounded-lg mb-6">
          <ExclamationTriangleIcon className="h-6 w-6 shrink-0 text-red-400" />
          <div>
            <h3 className="font-semibold text-red-300">
              Greška prilikom učitavanja
            </h3>
            <p className="text-red-400 text-sm mt-1">{error}</p>
          </div>
        </div>
      )}
      {!loading && !error && (
        <>
          {games.length > 0 ? (
            <GamesList games={games} />
          ) : (
            <div className="flex flex-col items-center justify-center py-20 text-gray-400">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-16 w-16 mb-4 opacity-70"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
                strokeWidth={1.5}
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="M9.75 9.75h4.5m-4.5 4.5h4.5M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
                />
              </svg>

              <h2 className="text-xl font-semibold mb-2">Nema igara</h2>
              <p className="text-center max-w-sm">
                Trenutno ne postoje igre u bazi. Dodaj nove igre kako bi se
                prikazale ovde.
              </p>
            </div>
          )}
        </>
      )}

      <div className="p-6">
        {showForm && <CreateGameForm setShowForm={setShowForm} />}
      </div>
    </div>
  );
}
