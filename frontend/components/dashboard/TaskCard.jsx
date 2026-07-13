import styles from "../../styles/dashboard/TaskCard.module.css";

function getPriorityClass(priority) {
  const normalizedPriority = priority.toLowerCase();

  if (normalizedPriority === "high") {
    return styles.high;
  }

  if (normalizedPriority === "medium") {
    return styles.medium;
  }

  return styles.low;
}

export default function TaskCard({
  task,
  onToggle,
  onEdit,
  onDelete,
  isToggleBusy = false,
  isDeleteBusy = false,
}) {
  return (
    <article className={styles.card} data-completed={task.isCompleted}>
      <button
        className={styles.completionButton}
        type="button"
        aria-pressed={task.isCompleted}
        aria-label={`Toggle completion for ${task.title}`}
        onClick={onToggle}
        disabled={isToggleBusy}
      >
        <span className={styles.completionMark} aria-hidden="true" />
      </button>

      <div className={styles.taskBody}>
        <h3>{task.title}</h3>
        <div className={styles.meta}>
          <span>{task.dueLabel}</span>
          <span>{task.category}</span>
        </div>
      </div>

      <span className={`${styles.priority} ${getPriorityClass(task.priority)}`}>
        {task.priority} priority
      </span>

      <div className={styles.actions}>
        <button type="button" onClick={onEdit} disabled={isToggleBusy || isDeleteBusy}>
          Edit
        </button>
        <button type="button" onClick={onDelete} disabled={!onDelete || isDeleteBusy}>
          {isDeleteBusy ? "Deleting..." : "Delete"}
        </button>
      </div>
    </article>
  );
}
