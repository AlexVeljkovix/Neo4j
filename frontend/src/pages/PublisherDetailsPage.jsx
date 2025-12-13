import { useParams } from "react-router-dom";
import { getGamesByPublisherId } from "../api/gameApi";
import { getPublisherById } from "../api/publisherApi";
import { useState, useEffect } from "react";
import { ExclamationTriangleIcon } from "@heroicons/react/24/outline";
import LargeGameCard from "../components/Games/LargeGameCard";
const PublisherDetailsPage = () => {
  const { id } = useParams();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [publisher, setPublisher] = useState(null);
  const [games, setGames] = useState([]);

  useEffect(() => {
    try {
      getPublisherById(id).then((data) => setPublisher(data));
      getGamesByPublisherId(id).then((data) => setGames(data));
    } catch (error) {
      setError(error.message);
    } finally {
      setLoading(false);
    }
  }, [id]);

  if (loading)
    return (
      <div className="p-8 text-white">
        <div className="animate-pulse bg-gray-800/50 p-6 rounded-lg w-full h-40" />
      </div>
    );

  if (error)
    return (
      <div className="p-8 text-red-400 flex items-center gap-3">
        <ExclamationTriangleIcon className="h-6 w-6 text-red-400" />
        <span>{error}</span>
      </div>
    );

  if (!publisher)
    return (
      <div className="p-8 text-white">
        <p>Nema dostupnih podataka o izdavaču.</p>
      </div>
    );

  return (
    <div className="p-8 text-white flex flex-col gap-6">
      {/* Publisher info */}

      <div className="bg-gray-800 p-6 rounded-lg shadow-md">
        <h1 className="text-4xl font-bold mb-2">{publisher.name}</h1>
        <p className="text-gray-300">Broj igara: {games.length}</p>
      </div>

      {/* Lista igara */}
      {games.length > 0 ? (
        <>
          <h2 className="text-3xl font-bold mb-2 text-black">Igre izdavača</h2>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            {games.map((game) => (
              <LargeGameCard key={game.id} game={game} />
            ))}
          </div>
        </>
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

          <h2 className="text-xl font-semibold mb-2">
            Nema igara za datog izdavača
          </h2>
          <p className="text-center max-w-sm">
            U bazi ne postoje igre za izabranog izdavača, izaberite drugog
            izdavača kako bi videli njegove igre
          </p>
        </div>
      )}
    </div>
  );
};

export default PublisherDetailsPage;
