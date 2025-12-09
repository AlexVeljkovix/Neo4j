import GameCard from "./GameCard.jsx";

const GamesList = ({ games }) => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
      {games.map((game) => (
        <div
          key={game.id}
          className="bg-gray-800 rounded-lg shadow-md p-4 text-white hover:bg-gray-700 transition"
        >
          <GameCard game={game} />
        </div>
      ))}
    </div>
  );
};

export default GamesList;
