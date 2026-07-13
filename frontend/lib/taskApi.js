const TASK_API_BASE_URL =
  process.env.NEXT_PUBLIC_TASK_API_URL?.replace(/\/$/, "") ?? "http://localhost:5001";
const REQUEST_TIMEOUT_MS = 10000;

async function readResponseBody(response) {
  if (response.status === 204) {
    return null;
  }

  const text = await response.text();
  if (!text) {
    return null;
  }

  const contentType = response.headers.get("content-type") ?? "";
  if (contentType.includes("application/json")) {
    return JSON.parse(text);
  }

  return text;
}

function getErrorMessage(body, fallbackMessage) {
  if (!body) {
    return fallbackMessage;
  }

  if (typeof body === "string") {
    return body;
  }

  if (typeof body.message === "string" && body.message.trim().length > 0) {
    return body.message;
  }

  if (typeof body.title === "string" && body.title.trim().length > 0) {
    return body.title;
  }

  return fallbackMessage;
}

async function request(path, options = {}) {
  const controller = new AbortController();
  const timeoutId = window.setTimeout(() => {
    controller.abort();
  }, REQUEST_TIMEOUT_MS);

  const response = await fetch(`${TASK_API_BASE_URL}${path}`, {
    headers: {
      Accept: "application/json",
      ...(options.body ? { "Content-Type": "application/json" } : {}),
      ...options.headers,
    },
    signal: controller.signal,
    ...options,
  }).catch((error) => {
    if (error.name === "AbortError") {
      throw new Error("The Task API request timed out.");
    }

    throw error;
  }).finally(() => {
    window.clearTimeout(timeoutId);
  });

  const body = await readResponseBody(response);

  if (!response.ok) {
    const error = new Error(
      getErrorMessage(body, `Request failed with status ${response.status}.`),
    );
    error.status = response.status;
    error.body = body;
    throw error;
  }

  return body;
}

export function getTasks({ status = "all", search = "" } = {}) {
  const query = new URLSearchParams();

  if (status) {
    query.set("status", status);
  }

  if (search.trim()) {
    query.set("search", search.trim());
  }

  const queryString = query.toString();
  return request(`/api/tasks${queryString ? `?${queryString}` : ""}`);
}

export function createTask(task) {
  return request("/api/tasks", {
    method: "POST",
    body: JSON.stringify(task),
  });
}

export function updateTask(id, task) {
  return request(`/api/tasks/${encodeURIComponent(id)}`, {
    method: "PUT",
    body: JSON.stringify(task),
  });
}

export function toggleTask(id) {
  return request(`/api/tasks/${encodeURIComponent(id)}/toggle`, {
    method: "PATCH",
  });
}

export function deleteTask(id) {
  return request(`/api/tasks/${encodeURIComponent(id)}`, {
    method: "DELETE",
  });
}

export function getDashboard(strategy = "basic") {
  const query = new URLSearchParams({ strategy });
  return request(`/api/dashboard?${query.toString()}`);
}
