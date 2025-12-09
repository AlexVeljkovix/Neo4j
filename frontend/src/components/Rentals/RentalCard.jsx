import React from "react";

const RentalCard = ({ rental }) => {
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
      <div className="mt-auto">
        <a
          href={`/rentals/${rental.Id}`}
          className="inline-flex items-center text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:ring-blue-500 shadow font-medium rounded-lg text-sm px-4 py-2.5 focus:outline-none"
        >
          Pogledaj detalje
          <svg
            className="w-4 h-4 ms-1.5 rtl:rotate-180 -me-0.5"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
          >
            <path
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              d="M19 12H5m14 0-4 4m4-4-4-4"
            />
          </svg>
        </a>
      </div>
    </div>
  );
};

export default RentalCard;
