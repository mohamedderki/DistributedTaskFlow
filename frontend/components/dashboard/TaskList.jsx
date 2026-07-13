import TaskCard from "./TaskCard";
import EmptyState from "../states/EmptyState";
import LoadingState from "../states/LoadingState";
import styles from "../../styles/dashboard/TaskList.module.css";

export default function TaskList({
  tasks,
  isLoading = false,
  errorMessage = "",
  mutationError = "",
  onCreateTask,
  onRetry,
  onToggleTask,
  onEditTask,
  onDeleteTask,
  togglingTaskId,
  deletingTaskId,
}) {
  return (
    <section className={styles.listPanel} aria-labelledby="task-list-title">
      <div className={styles.listHeader}>
        <div>
          <h2 id="task-list-title">Active Tasks</h2>
          <p>{tasks.length} visible tasks</p>
        </div>
      </div>

      {errorMessage ? (
        <div className={styles.errorBlock} role="alert">
          <p>{errorMessage}</p>
          <button type="button" onClick={onRetry}>
            Retry
          </button>
        </div>
      ) : mutationError ? (
        <p className={styles.mutationError} role="alert">
          {mutationError}
        </p>
      ) : null}

      {errorMessage ? null : isLoading ? (
        <LoadingState message="Loading tasks..." />
      ) : tasks.length === 0 ? (
        <EmptyState onCreateTask={onCreateTask} />
      ) : (
        <div className={styles.list}>
          {tasks.map((task) => (
            <TaskCard
              key={task.id}
              task={task}
              onToggle={() => onToggleTask(task.id)}
              onEdit={() => onEditTask(task)}
              onDelete={onDeleteTask ? () => onDeleteTask(task.id) : undefined}
              isToggleBusy={togglingTaskId === task.id}
              isDeleteBusy={deletingTaskId === task.id}
            />
          ))}
        </div>
      )}
    </section>
  );
}
