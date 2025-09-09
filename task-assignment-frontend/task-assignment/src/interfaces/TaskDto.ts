import type { TaskStatus } from "../query/__generated__/TaskListQuery.graphql";

export interface TaskDto {
  id: string;
  title: string;
  description?: string | null;
  status: TaskStatus;
  createdUtc: string;
  completedUtc?: string | null;
}