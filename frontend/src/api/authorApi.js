import { Get, Post, Put, Delete } from "./httpClient";

export function getAuthors() {
  return Get("/author");
}

export function getAuthorById(id) {
  return Get(`/author/by-id/${id}`);
}

export function getAuthorByName(firstname, lastname) {
  const params = new URLSearchParams({ firstname, lastname });
  return Get(`/author/by-name?${params.toString()}`);
}

export function getAuthorByGameId(gameId) {
  return Get(`/author/by-gameId/${gameId}`);
}

export function createAuthor(author) {
  return Post("/author", author);
}

export function updateAuthor(id, data) {
  return Put(`/author/${id}`, data);
}

export function deleteAuthor(id) {
  return Delete(`/author/${id}`);
}
