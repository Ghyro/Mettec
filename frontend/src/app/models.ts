export interface TaskDto {
  id: string;
  title: string;
  description: string;
  isCompleted: boolean;
}

export interface CreateTaskRequest {
  title: string;
  description: string;
}

export interface UpdateTaskStatusRequest {
  isCompleted: boolean;
}
