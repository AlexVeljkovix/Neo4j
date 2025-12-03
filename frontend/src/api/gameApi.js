import { Get, Post, Put, Delete } from "./httpClient";

export function getGames() {
  return Get("/game");
}

export function getGameById(id) {
  return Get(`game/by-id/${id}`);
}

export function getGamesByPublisherId(publisherId) {
  return httpGet(`/game/by-publisherId/${publisherId}`);
}
export function getGamesByMechanicId(mechanicId) {
  return httpGet(`/game/by-mechanicId/${mechanicId}`);
}
export function getGamesByName(title) {
  return httpGet(`/game/by-name?gameTitle=${title}`);
}
export function createGame(data) {
  return httpPost("/game", data);
}
export function addMechanicToGame(gameId, mechanicId) {
  return httpPost(`/game/${gameId}/mechanics/${mechanicId}`);
}
export function updateGame(id, data) {
  return httpPut(`/game/${id}`, data);
}
export function deleteGame(id) {
  return httpDelete(`/game/${id}`);
}
