import { Get, Post, Put, Delete } from "./httpClient";

export function getMechanics() {
  return Get("/mechanic");
}

export function getMechanicById(id) {
  return Get(`/mechanic/by-id/${id}`);
}

export function getMechanicByName(name) {
  return Get(`/mechanic/by-name?name=${name}`);
}

export function getMechanicByGameId(gameId) {
  return Get(`/mechanic/by-gameId/${gameId}`);
}

export function createMechanic(publisher) {
  return Post("/mechanic", publisher);
}

export function updateMechanic(id, data) {
  return Put(`/mechanic/${id}`, data);
}

export function deleteMechanic(id) {
  return Delete(`/mechanic/${id}`);
}
