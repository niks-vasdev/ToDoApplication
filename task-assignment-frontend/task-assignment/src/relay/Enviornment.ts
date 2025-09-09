import {
  Environment,
  Network,
  RecordSource,
  Store,
} from "relay-runtime";

// Define your GraphQL endpoint
const GRAPHQL_URL = import.meta.env.VITE_APP_BASE_URL || 'http://localhost:5262/graphql';

// Define the fetch function for Relay
async function fetchQuery(operation: any, variables: any) {
  const response = await fetch(GRAPHQL_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      query: operation.text,
      variables,
    }),
  });

  return await response.json();
}

// Create the Relay Environment
const relayEnvironment = new Environment({
  network: Network.create(fetchQuery),
  store: new Store(new RecordSource()),
});

export default relayEnvironment;