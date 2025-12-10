import { deletePublisher } from "../../api/publisherApi";
const PublisherCard = ({ publisher }) => {
  const handleClick = () => {
    deletePublisher(publisher.id);
  };
  return (
    <div className="bg-neutral-900/10 p-6 border border-gray-700 rounded-lg shadow-md h-60 flex flex-col">
      {publisher.ImageUrl && (
        <img
          src={publisher.ImageUrl}
          alt={publisher.name}
          className="w-full h-40 object-cover rounded-md mb-4"
        />
      )}
      <h4 className="mb-2 text-xl font-semibold tracking-tight text-white">
        {publisher.name}
      </h4>

      <div className="mt-auto flex justify-between">
        <a
          href={`/publishers/${publisher.Id}`}
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
export default PublisherCard;
