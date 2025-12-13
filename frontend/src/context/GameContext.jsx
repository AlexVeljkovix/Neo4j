import { useEffect, useState, useContext, createContext } from "react";
import { getGames } from "../api/gameApi";
const GameContext = createContext();
export const GameProvider = ({ children }) => {
  const [games, setGames] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    getGames()
      .then((data) => {
        setGames(data);
        setError(false);
        console.log(data);
      })
      .catch((error) => setError(error.message))
      .finally(() => setLoading(false));
  }, []);

  const addGame = (newGame) => setGames((prev) => [...prev, newGame]);
  const removeGame = (id) =>
    setGames((prev) => prev.filter((g) => g.id !== id));
  return (
    <GameContext.Provider
      value={{
        games,
        loading,
        error,
        addGame,
        removeGame,
      }}
    >
      {children}
    </GameContext.Provider>
  );
};
export const useGame = () => useContext(GameContext);
