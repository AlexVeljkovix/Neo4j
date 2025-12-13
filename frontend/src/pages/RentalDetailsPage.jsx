import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import { getRentalById, finishRental } from "../api/rentalApi";
import { ExclamationTriangleIcon } from "@heroicons/react/24/outline";

const RentalDetailsPage = () => {
  const { id } = useParams();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [rental, setRental] = useState(null);

  useEffect(() => {
    setLoading(true);

    getRentalById(id)
      .then((data) => setRental(data))
      .catch((error) => setError(error.message))
      .finally(() => setLoading(false));
  }, [id]);

  const formatDate = (d) => (d ? new Date(d).toLocaleString("sr-RS") : "—");

  const handleFinishRental = async () => {
    try {
      setLoading(true);
      const updated = await finishRental(id);

      // API vraća ažuriran rental, ali ako ne — sam ću ažurirati:
      setRental((prev) => ({
        ...prev,
        active: false,
        returnDate: new Date().toISOString(),
        ...updated, // ako API ipak vraća objekat
      }));
    } catch (err) {
      console.error(err);
      setError("Greška pri vraćanju igre.");
    } finally {
      setLoading(false);
    }
  };

  /* ---------------------------- LOADING SKELETON ---------------------------- */

  if (loading)
    return (
      <div className="p-8 text-white">
        <div className="animate-pulse bg-gray-800/50 p-6 rounded-2xl w-full h-60" />
      </div>
    );

  /* ---------------------------------- ERROR --------------------------------- */

  if (error)
    return (
      <div className="p-8 text-red-400 flex items-center gap-3">
        <ExclamationTriangleIcon className="h-6 w-6 text-red-400" />
        <span>{error}</span>
      </div>
    );

  /* ------------------------------ NO RENTAL -------------------------------- */

  if (!rental)
    return (
      <div className="p-8 text-gray-400">
        <p>Nema podataka o iznajmljivanju.</p>
      </div>
    );

  /* --------------------------------- CONTENT -------------------------------- */

  return (
    <div className="p-8 max-w-3xl mx-auto text-white">
      <div className="bg-gray-800 p-8 rounded-2xl shadow-md border border-gray-700">
        {/* Header + Status */}
        <div className="flex justify-between items-center mb-6">
          <h1 className="text-3xl font-bold">Detalji iznajmljivanja</h1>

          <span
            className={`px-4 py-1 rounded-full text-sm font-semibold border ${
              rental.active
                ? "bg-yellow-800/30 text-yellow-300 border-yellow-600"
                : "bg-green-800/30 text-green-300 border-green-600"
            }`}
          >
            {rental.active ? "Aktivno" : "Vraćeno"}
          </span>
        </div>

        {/* Info grid */}
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-6 text-gray-300">
          <div>
            <p className="text-sm text-gray-400">Ime osobe</p>
            <p className="font-semibold text-gray-200">{rental.personName}</p>
          </div>

          <div>
            <p className="text-sm text-gray-400">Telefon</p>
            <p className="font-semibold">{rental.personPhoneNumber}</p>
          </div>

          <div>
            <p className="text-sm text-gray-400">JMBG</p>
            <p className="font-semibold">{rental.personJMBG}</p>
          </div>

          <div>
            <p className="text-sm text-gray-400">Datum iznajmljivanja</p>
            <p className="font-semibold">{formatDate(rental.rentalDate)}</p>
          </div>

          <div>
            <p className="text-sm text-gray-400">Datum vraćanja</p>
            <p className="font-semibold">{formatDate(rental.returnDate)}</p>
          </div>

          <div>
            <p className="text-sm text-gray-400">Iznajmljena igra</p>
            <p className="font-semibold text-gray-200">{rental.gameName}</p>
          </div>
        </div>

        {/* Actions */}
        <div className="mt-8 flex justify-between">
          {/* Vrati igru */}
          {rental.active && (
            <button
              onClick={handleFinishRental}
              className="px-6 py-2 bg-green-700 border border-green-600 rounded-xl text-white font-semibold hover:bg-green-600 transition"
            >
              Vrati igru
            </button>
          )}

          {/* Link to game */}
          <Link
            to={`/games/${rental.gameId}`}
            className="px-6 py-2 bg-gray-700 rounded-xl text-white font-semibold hover:bg-gray-600 transition border border-gray-600"
          >
            Pogledaj igru
          </Link>
        </div>
      </div>
    </div>
  );
};

export default RentalDetailsPage;
