import { useParams, Link } from "react-router-dom";
import { useEffect, useState } from "react";
import SmallGameCard from "../components/Games/SmallGameCard";
import CreateRentalForm from "../components/Rentals/CreateRentalForm";
import { ExclamationTriangleIcon } from "@heroicons/react/24/outline";
import { useGame } from "../context/GameContext";
import { getGamesByMechanicId } from "../api/gameApi";

const GameDetailsPage = () => {
  const { id } = useParams();
  const { games } = useGame();
  const game = games.find((g) => g.id === id);

  const [similarGames, setSimilarGames] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);

  useEffect(() => {
    if (!game || !game.mechanics?.length) {
      setSimilarGames([]);
      return;
    }

    setLoading(true);
    setError(null);

    const mechanicIds = game.mechanics.map((m) => m.id);

    Promise.all(mechanicIds.map((id) => getGamesByMechanicId(id)))
      .then((results) => {
        const allGames = results.flat();
        const uniqueGamesMap = new Map();

        allGames.forEach((g) => {
          if (g.id !== game.id && !uniqueGamesMap.has(g.id)) {
            uniqueGamesMap.set(g.id, g);
          }
        });

        setSimilarGames([...uniqueGamesMap.values()]);
      })
      .catch(() =>
        setError("Došlo je do greške prilikom učitavanja sličnih igara.")
      )
      .finally(() => setLoading(false));
  }, [game]);

  if (!game)
    return (
      <div className="p-8 text-white">
        <p>Nema podataka o igri.</p>
      </div>
    );

  return (
    <div className="p-8 text-white flex flex-col md:flex-row gap-6">
      {error && (
        <div className="flex items-start gap-3 bg-red-900/20 border border-red-700 text-red-300 px-4 py-3 rounded-lg mb-6 w-full">
          <ExclamationTriangleIcon className="h-6 w-6 shrink-0 text-red-400" />
          <div>
            <h3 className="font-semibold text-red-300">
              Greška prilikom učitavanja
            </h3>
            <p className="text-red-400 text-sm mt-1">{error}</p>
          </div>
        </div>
      )}

      <div className="flex-1">
        <div className="bg-gray-800 p-6 rounded-lg shadow-md mb-6">
          <div className="flex justify-between">
            <h1 className="text-4xl font-bold mb-4">{game.title}</h1>
            {game.availableUnits > 0 && (
              <button
                onClick={() => setShowForm(true)}
                className="mb-4 p-4 hover:bg-gray-700 hover:cursor-pointer border rounded-2xl"
              >
                Novo iznajmljivanje
              </button>
            )}
          </div>

          <div className="bg-gray-700 border border-gray-600 p-4 rounded-lg mb-4">
            <p>
              <span className="font-bold">Težina:</span> {game.difficulty}
            </p>
            <p>
              <span className="font-bold">Dostupno:</span> {game.availableUnits}
            </p>
          </div>

          <p className="bg-gray-700 border border-gray-600 p-4 rounded-lg">
            {game.description}
          </p>
        </div>

        {similarGames.length > 0 && (
          <div className="mt-6">
            <h2 className="text-3xl text-black font-bold mb-2">Slične igre</h2>
            <div className="overflow-x-auto py-2">
              <div className="flex gap-4 snap-x snap-mandatory">
                {similarGames.map((g) => (
                  <div key={g.id} className="snap-start">
                    <SmallGameCard game={g} />
                  </div>
                ))}
              </div>
            </div>
          </div>
        )}
      </div>

      <div className="w-full md:w-64 bg-gray-800 p-4 rounded-lg shadow-md shrink-0 flex flex-col gap-4">
        {game.author && (
          <div>
            <p className="text-gray-300 font-semibold mb-1">Autor:</p>
            <Link
              to={`/authors/${game.author.id}`}
              className="text-blue-400 hover:underline"
            >
              {game.author.firstName} {game.author.lastName} (
              {game.author.country})
            </Link>
          </div>
        )}

        {game.publisher && (
          <div>
            <p className="text-gray-300 font-semibold mb-1">Izdavač:</p>
            <p className="text-gray-300">
              {game.publisher.name} ({game.publisher.country})
            </p>
          </div>
        )}

        {game.mechanics?.length > 0 && (
          <div>
            <p className="text-gray-300 font-semibold mb-1">Mehanike:</p>
            <ul className="list-disc list-inside text-gray-300">
              {game.mechanics.map((m) => (
                <li key={m.id}>{m.name}</li>
              ))}
            </ul>
          </div>
        )}
      </div>

      {showForm && (
        <CreateRentalForm setShowForm={setShowForm} defaultGame={game} />
      )}
    </div>
  );
};

export default GameDetailsPage;
