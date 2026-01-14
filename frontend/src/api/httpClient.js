export const API_URL = "http://localhost:5002/api";

export async function Get(endpoint) {
  const res = await fetch(`${API_URL}${endpoint}`);
  if (!res.ok) throw new Error(`HTTP error! Status: ${res.status}`);
  return res.json();
}

export async function Post(endpoint, body) {
  const res = await fetch(`${API_URL}${endpoint}`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  });

  const responseText = await res.text();
  console.log(`POST ${endpoint} - Status: ${res.status}`);
  console.log(`Response:`, responseText);

  if (!res.ok) {
    throw new Error(
      `HTTP error! Status: ${res.status}. Response: ${responseText}`
    );
  }

  if (!responseText || responseText.trim() === "") {
    console.warn("Empty response from server");
    return null;
  }

  try {
    return JSON.parse(responseText);
  } catch (error) {
    console.error(
      "Failed to parse JSON:",
      error,
      "Raw response:",
      responseText
    );
    throw new Error(`Invalid JSON response: ${responseText.substring(0, 100)}`);
  }
}

export async function Put(endpoint, body) {
  const res = await fetch(`${API_URL}${endpoint}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  });
  if (!res.ok) throw new Error(`HTTP error! Status: ${res.status}`);
  return res.json();
}

export async function Delete(endpoint) {
  const res = await fetch(`${API_URL}${endpoint}`, {
    method: "DELETE",
  });
  if (!res.ok) throw new Error(`HTTP error! Status: ${res.status}`);
  return res.json();
}
