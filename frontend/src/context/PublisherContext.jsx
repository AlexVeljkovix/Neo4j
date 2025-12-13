import {
  createContext,
  useContext,
  useState,
  useEffect,
  Children,
} from "react";
import {
  getPublishers,
  createPublisher,
  deletePublisher,
} from "../api/publisherApi";

const PublisherContext = createContext();
export const PublisherProvider = ({ children }) => {
  const [publishers, setPublishers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchPublishers = () => {
    getPublishers()
      .then((data) => {
        setPublishers(data);
        setError(null);
      })
      .catch((error) => setError(error.message))
      .finally(() => setLoading(false));
  };
  useEffect(() => fetchPublishers(), []);

  const addPublisher = (newPublisher) => {
    setPublishers((prev) => [...prev, newPublisher]);
  };

  const removePublisher = (id) => {
    setPublishers((prev) => prev.filter((p) => p.id !== id));
  };

  return (
    <PublisherContext.Provider
      value={{
        publishers,
        loading,
        error,
        fetchPublishers,
        addPublisher,
        removePublisher,
      }}
    >
      {children}
    </PublisherContext.Provider>
  );
};

export const usePublishers = () => useContext(PublisherContext);
