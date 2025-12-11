import { deleteGame } from "../../api/gameApi";
const GameCard = ({ game }) => {
  console.log(game);
  const handleClick = () => {
    deleteGame(game.id);
  };
  const cardColors = [
    "bg-green-400",
    "bg-yellow-400",
    "bg-orange-400",
    "bg-red-400",
  ];
  const diffIndex = Math.min(
    Math.floor(game.difficulty) - 1,
    cardColors.length - 1
  );
  return (
    <div className="relative bg-neutral-900/10 p-6 border border-gray-700 rounded-lg shadow-md h-60 flex flex-col">
      <div
        className={`w-4 h-4 rounded absolute top-2 right-2 ${cardColors[diffIndex]}`}
      ></div>
      {game.ImageUrl && (
        <img
          src={game.ImageUrl}
          alt={game.Title}
          className="w-full h-40 object-cover rounded-md mb-4"
        />
      )}
      <h4 className="mb-2 text-xl font-semibold tracking-tight text-white">
        {game.title}
      </h4>

      {game.author && (
        <p className="text-gray-400 text-sm mb-1">
          Autor: {game.author.firstName} {game.author.lastName} (
          {game.author.country})
        </p>
      )}
      {game.publisher && (
        <p className="text-gray-400 text-sm mb-3">
          Izdavač: {game.publisher.name}
        </p>
      )}
      <p className="text-gray-400 text-sm mb-3">
        Dostupno: {game.availableUnits}
      </p>

      <div className="mt-auto flex justify-between">
        <a
          href={`/games/${game.id}`}
          className="inline-flex items-center text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:ring-blue-500 shadow font-medium rounded-lg text-sm px-4 py-2.5 focus:outline-none"
        >
          Pogledaj detalje
        </a>
        <button
          onClick={handleClick}
          className="inline-flex items-center text-white bg-red-600 hover:bg-red-700 focus:ring-4 focus:ring-red-500 shadow font-medium rounded-lg text-sm px-4 py-2.5 focus:outline-none hover: cursor-pointer"
        >
          Obriši
        </button>
      </div>
    </div>
  );
};

export default GameCard;
