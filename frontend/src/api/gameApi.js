import { Get, Post, Put, Delete } from "./httpClient";

export function getGames() {
  return Get("/game");
}
export function getGameById(id) {
  return Get(`game/by-id/${id}`);
}
export function getGamesByPublisherId(publisherId) {
  return Get(`/game/by-publisherId/${publisherId}`);
}
export function getGamesByMechanicId(mechanicId) {
  return Get(`/game/by-mechanicId/${mechanicId}`);
}
export function getGamesByName(title) {
  return Get(`/game/by-name?gameTitle=${title}`);
}
export function createGame(data) {
  return Post("/game", data);
}
export function addMechanicToGame(gameId, mechanicId) {
  return Post(`/game/${gameId}/mechanics/${mechanicId}`);
}
export function updateGame(id, data) {
  return Put(`/game/${id}`, data);
}
export function deleteGame(id) {
  return Delete(`/game/${id}`);
}
