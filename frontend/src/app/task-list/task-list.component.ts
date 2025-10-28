import { Component, OnInit } from '@angular/core';
import { TaskDto } from '../models';
import { TaskService } from '../task.service';
import { CreateTaskService } from '../create-task/create-task.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html'
})
export class TaskListComponent implements OnInit {
  tasks: TaskDto[] = [];
  loading = false;
  error: string | null = null;

  constructor(private taskService: TaskService, private createTaskService: CreateTaskService) {}

  ngOnInit(): void {
    this.loadTasks();

    this.createTaskService.taskCreated$.subscribe(task => {
      this.tasks.push(task);
    });
  }

  loadTasks() {
    this.loading = true;
    this.error = null;
    this.taskService.getTasks().subscribe({
      next: tasks => {
        this.tasks = tasks;
        this.loading = false;
      },
      error: err => {
        this.error = 'Failed to load tasks';
        this.loading = false;
      }
    });
  }

  toggleCompleted(task: TaskDto, isCompleted: boolean) {
    this.taskService.updateStatus(task.id, isCompleted).subscribe({
      next: updatedTask => {
        task.isCompleted = updatedTask.isCompleted;
      },
      error: err => {
        this.error = 'Failed to update task status';
      }
    });
  }
}
