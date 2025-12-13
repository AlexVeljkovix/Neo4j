import { deleteAuthor } from "../../api/authorApi";
import { Link } from "react-router-dom";
import { useAuthor } from "../../context/AuthorContext";
const AuthorCard = ({ author }) => {
  const { removeAuthor } = useAuthor();
  const handleClick = async () => {
    await deleteAuthor(author.id);
    removeAuthor(author.id);
  };
  return (
    <div className="bg-neutral-900/10 p-6 border border-gray-700 rounded-lg shadow-md h-60 flex flex-col">
      {author.ImageUrl && (
        <img
          src={author.ImageUrl}
          alt={author.firstName + " " + author.lastName}
          className="w-full h-40 object-cover rounded-md mb-4"
        />
      )}
      <h4 className="mb-2 text-xl font-semibold tracking-tight text-white">
        {author.firstName} {author.lastName}
      </h4>
      <p className="text-gray-400 text-sm mb-3">Država: {author.country}</p>

      <div className="mt-auto flex justify-between">
        <Link
          to={`/authors/${author.id}`}
          className="inline-flex items-center text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:ring-blue-500 shadow font-medium rounded-lg text-sm px-4 py-2.5 focus:outline-none"
        >
          Pogledaj detalje
        </Link>
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

export default AuthorCard;
