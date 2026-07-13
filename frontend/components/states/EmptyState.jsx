import styles from "../../styles/states/EmptyState.module.css";

export default function EmptyState({
  title = "No tasks yet",
  message = "Create your first task to start organizing your work.",
  onCreateTask,
}) {
  return (
    <div className={styles.emptyState}>
      <div className={styles.icon} aria-hidden="true">
        <span />
        <span />
        <span />
      </div>
      <h3>{title}</h3>
      <p>{message}</p>
      <button type="button" onClick={onCreateTask}>
        Create Task
      </button>
    </div>
  );
}
