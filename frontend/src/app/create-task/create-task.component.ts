import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TaskService } from '../task.service';
import { CreateTaskRequest } from '../models';
import { CreateTaskService } from './create-task.service';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html'
})
export class CreateTaskComponent {
  form = this.fb.group({
    title: ['', Validators.required],
    description: ['']
  });

  creating = false;
  error: string | null = null;

  constructor(private fb: FormBuilder,
    private taskService: TaskService,
    private createTaskService: CreateTaskService) {}

  submit() {
    if (this.form.invalid) return;

    this.creating = true;
    this.error = null;

    const request: CreateTaskRequest = this.form.value as CreateTaskRequest;

    this.taskService.createTask(request).subscribe({
      next: (createdTask) => {
        this.creating = false;
        this.form.reset();
        this.createTaskService.emitTaskCreated(createdTask);
      },
      error: (_) => {
        this.creating = false;
        this.error = 'Failed to create task';
      }
    });
  }
}
