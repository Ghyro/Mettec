import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskListComponent } from './task-list.component';
import { TaskService } from '../task.service';
import { of, throwError } from 'rxjs';
import { TaskDto } from '../models';

describe('TaskListComponent', () => {
  let component: TaskListComponent;
  let fixture: ComponentFixture<TaskListComponent>;
  let mockTaskService: jasmine.SpyObj<TaskService>;

  const mockTasks: TaskDto[] = [
    { id: '1', title: 'Task 1', description: 'Desc 1', isCompleted: false },
    { id: '2', title: 'Task 2', description: 'Desc 2', isCompleted: true }
  ];

  beforeEach(async () => {
    mockTaskService = jasmine.createSpyObj('TaskService', ['getTasks', 'updateStatus']);

    await TestBed.configureTestingModule({
      declarations: [TaskListComponent],
      providers: [{ provide: TaskService, useValue: mockTaskService }]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load tasks successfully', () => {
    mockTaskService.getTasks.and.returnValue(of(mockTasks));

    component.loadTasks();

    expect(component.loading).toBe(false);
    expect(component.tasks.length).toBe(2);
    expect(component.error).toBeNull();
    expect(mockTaskService.getTasks).toHaveBeenCalled();
  });

  it('should handle loadTasks error', () => {
    mockTaskService.getTasks.and.returnValue(throwError(() => new Error('Failed')));

    component.loadTasks();

    expect(component.loading).toBe(false);
    expect(component.tasks.length).toBe(0);
    expect(component.error).toBe('Failed to load tasks');
  });

  it('should toggle task completion successfully', () => {
    const task = { ...mockTasks[0] };
    const updatedTask = { ...task, isCompleted: true };
    mockTaskService.updateStatus.and.returnValue(of(updatedTask));

    component.toggleCompleted(task, true);

    expect(task.isCompleted).toBe(true);
    expect(mockTaskService.updateStatus).toHaveBeenCalledWith(task.id, true);
  });

  it('should handle toggleCompleted error', () => {
    const task = { ...mockTasks[0] };
    mockTaskService.updateStatus.and.returnValue(throwError(() => new Error('Failed')));

    component.toggleCompleted(task, true);

    expect(component.error).toBe('Failed to update task status');
  });
});
