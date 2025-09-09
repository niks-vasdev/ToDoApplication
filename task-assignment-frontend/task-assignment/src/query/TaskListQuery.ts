import { graphql } from "react-relay";

export const TaskListQuery = graphql`
  query TaskListQuery {
    getAllTasks {
      id
      title
      description
      status
      createdUtc
      completedUtc
    }
  }
`;