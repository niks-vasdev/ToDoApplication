import { graphql } from "react-relay";

export const UpdateTaskStatusMutation = graphql`
  mutation UpdateTaskStatusMutation($input: UpdateTaskStatusInput!) {
    updateTaskStatus(input: $input) {
      id
      status
      completedUtc
    }
  }
`;