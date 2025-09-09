export interface TaskDto {
  id: string;
  title: string;
  description?: string;
  status: "PENDING" | "COMPLETED";
  createdUtc: string;
  completedUtc?: string;
}