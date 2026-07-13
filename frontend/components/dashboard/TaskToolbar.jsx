import styles from "../../styles/dashboard/TaskToolbar.module.css";

const filters = [
  { id: "all", label: "All" },
  { id: "open", label: "Open" },
  { id: "completed", label: "Completed" },
];

export default function TaskToolbar({ activeStatus, onStatusChange }) {
  return (
    <div className={styles.toolbar} aria-label="Task status filters">
      {filters.map((filter) => {
        const isActive = filter.id === activeStatus;

        return (
          <button
            key={filter.id}
            type="button"
            className={`${styles.filterButton} ${isActive ? styles.active : ""}`}
            aria-pressed={isActive}
            onClick={() => onStatusChange(filter.id)}
          >
            {filter.label}
          </button>
        );
      })}
    </div>
  );
}
