import { PlusIcon } from "@heroicons/react/20/solid";
import { useEffect, useState } from "react";
import CardTW from "../components/CardTW";
import MultipleSelect from "../components/MultipleSelect";
import { createGame, getGames } from "../api/gameApi";
import { getPublishers } from "../api/publisherApi";
import { getMechanics } from "../api/mechanicsApi";
import { getAuthors } from "../api/authorApi";

export default function HomePage() {
  const [games, setGames] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const [mechanicsSelected, setMechanicsSelected] = useState([]);
  const [author, setAuthor] = useState("");
  const [publisher, setPublisher] = useState("");

  const [mechanics, setMechanics] = useState([]);
  const [authors, setAuthors] = useState([]);
  const [publishers, setPublishers] = useState([]);

  useEffect(() => {
    getGames()
      .then(setGames)
      .catch((err) => setError(err.message))
      .finally(() => {
        setLoading(false);
        setError(null);
      });
  }, []);

  const loadFormData = async () => {
    try {
      const [mechanicsData, authorsData, publishersData] = await Promise.all([
        getMechanics(),
        getAuthors(),
        getPublishers(),
      ]);
      setMechanics(mechanicsData);
      setAuthors(authorsData);
      setPublishers(publishersData);
    } catch (err) {
      console.error("Error fetching data:", err);
    }
  };

  const submitForm = (e) => {
    e.preventDefault();
    createGame({
      title: title,
      description: description,
      mechanicIds: mechanicSelected.map((m) => m.id),
      authorId: author,
      publisherId: publisher,
    }).then((res) => {
      console.log(res);
      setShowForm(false);
    });
  };

  return (
    <div className="mx-6 my-4">
      {/* Naslov i dugme */}
      <div className="flex flex-col sm:flex-row sm:justify-between sm:items-center mb-6">
        <h2 className="text-2xl font-bold sm:text-3xl sm:tracking-tight mb-4 sm:mb-0">
          Sve dostupne igre
        </h2>
        <button
          onClick={() => {
            loadFormData();
            setShowForm(true);
          }}
          type="button"
          className="inline-flex items-center rounded-md bg-gray-800 hover:cursor-pointer px-4 py-2 text-sm font-semibold text-white hover:bg-gray-700 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-900"
        >
          <PlusIcon aria-hidden="true" className="mr-1.5 h-5 w-5" />
          Dodaj novu igru
        </button>
      </div>

      {/* Status */}
      {loading && <p className="text-gray-400">Učitavanje...</p>}
      {error && <p className="text-red-500">Greška: {error}</p>}

      {/* Kartice */}
      {games.length ? (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          {games.map((game) => (
            <div
              key={game.id}
              className="bg-gray-800 rounded-lg shadow-md p-4 text-white hover:bg-gray-700 transition"
            >
              <CardTW game={game} />
            </div>
          ))}
        </div>
      ) : (
        <div className="flex flex-col items-center justify-center py-20 text-gray-400">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="h-16 w-16 mb-4 opacity-70"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            strokeWidth={1.5}
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M9.75 9.75h4.5m-4.5 4.5h4.5M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
            />
          </svg>

          <h2 className="text-xl font-semibold mb-2">Nema igara</h2>
          <p className="text-center max-w-sm">
            Trenutno ne postoje igre u bazi. Dodaj nove igre kako bi se
            prikazale ovde.
          </p>
        </div>
      )}

      <div className="p-6">
        {/* BACKDROP */}
        {showForm && (
          <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
            {/* MODAL */}
            <div className="bg-white p-6 rounded-lg shadow-lg w-full max-w-md">
              <h2 className="text-xl font-semibold mb-4">Novi unos</h2>

              <form className="flex flex-col gap-3">
                <input
                  type="text"
                  onChange={(e) => setTitle(e.target.value)}
                  placeholder="Naziv igre"
                  className="border p-2 rounded"
                />

                <input
                  type="text"
                  onChange={(e) => setDescription(e.target.value)}
                  placeholder="Opis igre"
                  className="border p-2 rounded"
                />
                <MultipleSelect
                  mechanics={mechanics}
                  selected={mechanicsSelected}
                  setSelected={setMechanicsSelected}
                />
                <select
                  className="border p-2 rounded"
                  value={author}
                  onChange={(e) => setAuthor(e.target.value)}
                >
                  <option value="">Izaberite autora</option>
                  {authors.map((a) => (
                    <option key={a.id} value={a.id}>
                      {a.firstName} {a.lastName}
                    </option>
                  ))}
                </select>
                <select
                  className="border p-2 rounded"
                  value={publisher}
                  onChange={(e) => setPublisher(e.target.value)}
                >
                  <option value="">Izaberite izdavaca</option>
                  {publishers.map((p) => (
                    <option key={p.id} value={p.id}>
                      {p.name}
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
        )}
      </div>
    </div>
  );
}
