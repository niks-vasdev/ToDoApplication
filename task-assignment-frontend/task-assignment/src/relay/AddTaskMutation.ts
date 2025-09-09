import { graphql } from "react-relay";

export const AddTaskMutation = graphql`
  mutation AddTaskMutation($input: CreateTaskInput!) {
    createTask(input: $input) {
      id
      title
      description
      status
      createdUtc
      completedUtc
    }
  }
`;
