import { Get, Post, Put, Delete } from "./httpClient";

export function getRentals() {
  return Get("/rental");
}

export function getActiveRentals() {
  return Get("/rental/active");
}

export function createRental(data) {
  return Post("/rental", data);
}

export function finishRental(rentalId) {
  return Put(`/rental/finish/${rentalId}`);
}

export function deleteRental(rentalId) {
  return Delete(`/rental/${rentalId}`);
}
