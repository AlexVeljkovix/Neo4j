import { useState, useEffect, use } from "react";
import { createRental } from "../../api/rentalApi";
import { getGames } from "../../api/gameApi";

const CreateRentalForm = ({ setShowForm }) => {
  const [gamesData, setGamesData] = useState([]);
  const [name, setName] = useState("");
  const [JMBG, setJMBG] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [gameId, setGameId] = useState("");

  useEffect(() => {
    getGames().then((data) => {
      setGamesData(data);
    });
  }, []);

  const submitForm = (e) => {
    e.preventDefault();
    createRental({
      personName: name,
      personPhoneNumber: phoneNumber,
      personJMBG: JMBG,
      gameId: gameId,
    }).then((res) => {
      console.log(res);
      setShowForm(false);
    });
  };

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-full max-w-md">
        <h2 className="text-xl font-semibold mb-4">Novo iznajmljivanje</h2>

        <form className="flex flex-col gap-3">
          <input
            type="text"
            onChange={(e) => setName(e.target.value)}
            placeholder="Ime i prezime"
            className="border p-2 rounded"
          />

          <input
            type="text"
            onChange={(e) => setJMBG(e.target.value)}
            placeholder="JMBG"
            className="border p-2 rounded"
          />
          <input
            type="text"
            onChange={(e) => setPhoneNumber(e.target.value)}
            placeholder="Broj telefona"
            className="border p-2 rounded"
          />
          <select
            className="border p-2 rounded"
            value={gameId}
            onChange={(e) => setGameId(e.target.value)}
          >
            <option value="">Izaberite igru</option>
            {gamesData.map((game) => (
              <option key={game.id} value={game.id}>
                {game.title}
              </option>
            ))}
          </select>
          <div className="flex justify-end gap-3 mt-3">
            <button
              type="button"
              onClick={() => setShowForm(false)}
              className="px-4 py-2 bg-gray-300 hover:bg-gray-200 hover:cursor-pointer rounded"
            >
              Otkaži
            </button>

            <button
              type="submit"
              onClick={submitForm}
              className="px-4 py-2 bg-gray-800 hover:bg-gray-700 hover:cursor-pointer text-white rounded"
            >
              Sačuvaj
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default CreateRentalForm;
