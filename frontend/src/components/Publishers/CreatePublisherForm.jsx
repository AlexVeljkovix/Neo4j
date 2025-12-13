import { useState, useEffect } from "react";
import { createPublisher } from "../../api/publisherApi";
import { usePublishers } from "../../context/PublisherContext";

const CreatePublisherForm = ({ setShowForm }) => {
  const [name, setName] = useState("");
  const [country, setCountry] = useState("");
  const { addPublisher } = usePublishers();
  const submitForm = async (e) => {
    e.preventDefault();
    const publisher = await createPublisher({
      name: name,
      country: country,
    });
    addPublisher(publisher);
    setShowForm(false);
  };

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-full max-w-md">
        <h2 className="text-xl font-semibold mb-4">Dodaj novog izdavaca</h2>

        <form onSubmit={submitForm} className="flex flex-col gap-3">
          <input
            type="text"
            onChange={(e) => setName(e.target.value)}
            placeholder="Ime izdavaca"
            className="border p-2 rounded"
          />
          <input
            type="text"
            onChange={(e) => setCountry(e.target.value)}
            placeholder="Država"
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

export default CreatePublisherForm;
