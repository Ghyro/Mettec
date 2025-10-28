import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { TaskDto, CreateTaskRequest, UpdateTaskStatusRequest } from './models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private base = environment.apiUrl + '/Task';

  constructor(private http: HttpClient) {}

  getTasks(): Observable<TaskDto[]> {
    return this.http.get<TaskDto[]>(this.base);
  }

  createTask(request: CreateTaskRequest) {
    return this.http.post<TaskDto>(this.base + '/create', request);
  }

  updateStatus(id: string, isCompleted: boolean) {
    const body: UpdateTaskStatusRequest = { isCompleted };
    return this.http.put<TaskDto>(`${this.base}/${id}/status`, body);
  }
}
