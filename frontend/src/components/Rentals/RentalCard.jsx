import { Link } from "react-router-dom";
import { deleteRental, finishRental } from "../../api/rentalApi";
import { useRental } from "../../context/RentalContext";
import { useGame } from "../../context/GameContext";
const RentalCard = ({ rental, active }) => {
  const { removeRental, finishRentalC } = useRental();
  const { updateAvailableUnits } = useGame();
  const handleClick = () => {
    finishRental(rental.id);
    finishRentalC(rental.id);
    updateAvailableUnits(rental.gameId, +1);
  };
  const handleDelete = () => {
    deleteRental(rental.id);
    removeRental(rental.id);
  };

  return (
    <div className="bg-neutral-900/10 p-6 border border-gray-700 rounded-lg shadow-md h-60 flex flex-col">
      <h4 className="mb-2 text-xl font-semibold tracking-tight text-white">
        Igra: {rental.gameName}
      </h4>
      <p className="text-gray-400 text-sm mb-1">Ime: {rental.personName} </p>
      <p className="text-gray-400 text-sm mb-1">
        Broj telefona: {rental.personPhoneNumber}
      </p>
      <p className="text-gray-400 text-sm mb-1">
        Datum iznajmljivanja:{" "}
        {new Date(rental.rentalDate).toLocaleDateString("sr-RS")}
      </p>
      <div className="mt-auto flex justify-between">
        <Link
          to={`/rentals/${rental.id}`}
          className="inline-flex text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:ring-blue-500 shadow font-medium rounded-lg text-sm px-4 py-2.5 focus:outline-none"
        >
          Pogledaj detalje
        </Link>
        {active ? (
          <button
            onClick={handleClick}
            className="inline-flex items-center text-white bg-green-600 hover:bg-green-700 focus:ring-4 focus:ring-green-500 shadow font-medium rounded-lg text-sm px-4 py-2.5 focus:outline-none hover: cursor-pointer"
          >
            Vrati igru
          </button>
        ) : (
          <button
            onClick={handleDelete}
            className="inline-flex items-center text-white bg-red-600 hover:bg-red-700 focus:ring-4 focus:ring-red-500 shadow font-medium rounded-lg text-sm px-4 py-2.5 focus:outline-none hover: cursor-pointer"
          >
            Obriši
          </button>
        )}
      </div>
    </div>
  );
};

export default RentalCard;
