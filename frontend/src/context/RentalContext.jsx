import {
  useEffect,
  useState,
  createContext,
  useContext,
  Children,
} from "react";
import { getRentals } from "../api/rentalApi";

const RentalContext = createContext();
export const RentalProvider = ({ children }) => {
  const [rentals, setRentals] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    getRentals()
      .then((data) => {
        setRentals(data);
        setError(false);
      })
      .catch((error) => {
        setError(error.message);
      })
      .finally(() => setLoading(false));
  }, []);
  const addRental = (newRental) => {
    setRentals((prev) => [...prev, newRental]);
  };
  const removeRental = (id) => {
    setRentals((prev) => prev.filter((r) => r.id !== id));
  };

  const finishRentalC = (id) => {
    setRentals((prev) =>
      prev.map((r) =>
        r.id === id
          ? {
              ...r,
              active: false,
              returnDate: new Date().toISOString(),
            }
          : r
      )
    );
  };

  return (
    <RentalContext.Provider
      value={{
        rentals,
        loading,
        error,
        addRental,
        removeRental,
        finishRentalC,
      }}
    >
      {children}
    </RentalContext.Provider>
  );
};

export const useRental = () => useContext(RentalContext);
