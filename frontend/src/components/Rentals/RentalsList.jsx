import RentalCard from "./RentalCard";

const RentalsList = ({ rentals }) => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
      {rentals.map((rental) => (
        <div
          key={rental.id}
          className="bg-gray-800 rounded-lg shadow-md p-4 text-white hover:bg-gray-700 transition"
        >
          <RentalCard rental={rental} />
        </div>
      ))}
    </div>
  );
};

export default RentalsList;
