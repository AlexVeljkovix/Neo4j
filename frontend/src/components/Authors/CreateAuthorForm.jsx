import { useEffect, useState } from "react";
import { createAuthor } from "../../api/authorApi";
import { useAuthor } from "../../context/AuthorContext";

const CreateAuthorForm = ({ setShowForm }) => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [country, setCountry] = useState("");
  const { addAuthor } = useAuthor();
  const submitForm = (e) => {
    e.preventDefault();
    createAuthor({
      firstName: firstName,
      lastName: lastName,
      country: country,
    }).then((res) => {
      addAuthor(res);
      console.log(res);
      setShowForm(false);
    });
  };

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-full max-w-md">
        <h2 className="text-xl font-semibold mb-4">Dodaj novu igru</h2>

        <form onSubmit={submitForm} className="flex flex-col gap-3">
          <input
            type="text"
            onChange={(e) => setFirstName(e.target.value)}
            placeholder="Ime autora"
            className="border p-2 rounded"
          />

          <input
            type="text"
            onChange={(e) => setLastName(e.target.value)}
            placeholder="Prezime autora"
            className="border p-2 rounded"
          />
          <input
            type="text"
            onChange={(e) => setCountry(e.target.value)}
            placeholder="Drzava porekla autora"
            className="border p-2 rounded"
          />

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

export default CreateAuthorForm;
