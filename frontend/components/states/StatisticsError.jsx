import styles from "../../styles/states/StatisticsError.module.css";

export default function StatisticsError({
  message = "Statistics are temporarily unavailable. Your tasks can still be managed.",
  onRetry,
}) {
  return (
    <div className={styles.errorState} role="status" aria-live="polite">
      <div className={styles.message}>
        <span aria-hidden="true">i</span>
        <p>{message}</p>
      </div>
      <button type="button" onClick={onRetry}>
        Retry
      </button>
    </div>
  );
}
