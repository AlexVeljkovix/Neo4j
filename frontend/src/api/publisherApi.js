import { Get, Post, Put, Delete } from "./httpClient";

export function getPublishers() {
  return Get("/publisher");
}

export function getPublisherById(id) {
  return Get(`/publisher/by-id/${id}`);
}

export function getPublisherByName(name) {
  return Get(`/publisher/by-name?name=${name}`);
}

export function getPublisherByGameId(gameId) {
  return Get(`/publisher/by-gameId/${gameId}`);
}

export function createPublisher(publisher) {
  return Post("/publisher", publisher);
}

export function updatePublisher(id, data) {
  return Put(`/publisher/${id}`, data);
}

export function deletePublisher(id) {
  return Delete(`/publisher/${id}`);
}
