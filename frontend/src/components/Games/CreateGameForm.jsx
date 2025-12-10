import MultipleSelect from "../MultipleSelect";
import { useEffect, useState } from "react";
import { createGame } from "../../api/gameApi";
import { getMechanics } from "../../api/mechanicApi";
import { getAuthors } from "../../api/authorApi";
import { getPublishers } from "../../api/publisherApi";

const CreateGameForm = ({ setShowForm }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [mechanicsSelected, setMechanicsSelected] = useState([]);
  const [author, setAuthor] = useState("");
  const [publisher, setPublisher] = useState("");

  const [mechanicsData, setMechanicsData] = useState([]);
  const [authorsData, setAuthorsData] = useState([]);
  const [publishersData, setPublishersData] = useState([]);

  const loadFormData = async () => {
    try {
      const [mechanicsData, authorsData, publishersData] = await Promise.all([
        getMechanics(),
        getAuthors(),
        getPublishers(),
      ]);
      setMechanicsData(
        mechanicsData.sort((a, b) => a.name.localeCompare(b.name, "sr"))
      );
      setAuthorsData(
        authorsData.sort((a, b) =>
          `${a.firstName} ${a.lastName}`.localeCompare(
            `${b.firstName} ${b.lastName}`,
            "sr"
          )
        )
      );
      setPublishersData(
        publishersData.sort((a, b) => a.name.localeCompare(b.name, "sr"))
      );
    } catch (err) {
      console.error("Error fetching data:", err);
    }
  };

  const submitForm = (e) => {
    e.preventDefault();
    createGame({
      title: title,
      description: description,
      mechanicIds: mechanicsSelected.map((m) => m.id),
      authorId: author,
      publisherId: publisher,
    }).then((res) => {
      console.log(res);
      setShowForm(false);
    });
  };

  useEffect(() => {
    loadFormData();
  }, []);

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-full max-w-md">
        <h2 className="text-xl font-semibold mb-4">Dodaj novu igru</h2>

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
            mechanics={mechanicsData}
            selected={mechanicsSelected}
            setSelected={setMechanicsSelected}
          />
          <select
            className="border p-2 rounded"
            value={author}
            onChange={(e) => setAuthor(e.target.value)}
          >
            <option value="">Izaberite autora</option>
            {authorsData.map((a) => (
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
            {publishersData.map((p) => (
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
  );
};

export default CreateGameForm;
