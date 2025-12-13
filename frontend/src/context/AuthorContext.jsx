import { useEffect, useState, createContext, useContext } from "react";
import { getAuthors } from "../api/authorApi";

const AuthorContext = createContext();
export const AuthorProvider = ({ children }) => {
  const [authors, setAuthors] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    getAuthors()
      .then((data) => {
        setAuthors(data);
        setError(false);
      })
      .catch((error) => setError(error.message))
      .finally(() => setLoading(false));
  }, []);
  const addAuthor = (newAuthor) => {
    setAuthors((prev) => [...prev, newAuthor]);
  };
  const removeAuthor = (id) => {
    setAuthors((prev) => prev.filter((a) => a.id !== id));
  };
  return (
    <AuthorContext.Provider
      value={{ authors, loading, error, addAuthor, removeAuthor }}
    >
      {children}
    </AuthorContext.Provider>
  );
};
export const useAuthor = () => useContext(AuthorContext);
