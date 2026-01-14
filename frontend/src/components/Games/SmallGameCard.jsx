import { Link } from "react-router-dom";

const SmallGameCard = ({ game }) => {
  return (
    <Link to={`/games/${game.id}`} className="block min-w-[220px] shrink-0">
      <div className="bg-gray-800 p-4 rounded-lg shadow-md hover:shadow-lg hover:bg-neutral-700 transition-shadow duration-200 h-full flex flex-col justify-between">
        <h3 className="text-lg font-bold text-white mb-2 truncate">
          {game.title}
        </h3>

        {game.author && (
          <p className="text-gray-300 text-sm mb-1 truncate">
            Autor: {game.author.firstName} {game.author.lastName}
          </p>
        )}

        {game.publisher && (
          <p className="text-gray-300 text-sm mb-1 truncate">
            Izdavač: {game.publisher.name}
          </p>
        )}

        <p className="text-gray-400 text-sm font-semibold mt-auto">
          Težina: {game.difficulty}
        </p>
      </div>
    </Link>
  );
};

export default SmallGameCard;
