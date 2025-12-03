import { Get, Post, Put, Delete } from "./httpClient";

export function getRentals() {
  return Get("/rentals");
}

export function createRentral(data) {
  return Post("/rentals", data);
}

export function finishRental(rentalId) {
  return Put(`/rentals/finish/${rentalId}`);
}

export function deleteRental(rentalId) {
  return Delete(`/rentals/${rentalId}`);
}
