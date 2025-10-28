import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { TaskDto } from '../models';

@Injectable({ providedIn: 'root' })
export class CreateTaskService {
  private taskCreatedSource = new Subject<TaskDto>();
  taskCreated$ = this.taskCreatedSource.asObservable();

  emitTaskCreated(task: TaskDto) {
    this.taskCreatedSource.next(task);
  }
}
