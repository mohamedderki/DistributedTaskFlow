import styles from "../../styles/states/LoadingState.module.css";

export default function LoadingState({ message = "Loading tasks..." }) {
  return (
    <div className={styles.loadingState} role="status" aria-live="polite">
      <p>{message}</p>
      <div className={styles.skeletonList} aria-hidden="true">
        {[1, 2, 3, 4].map((item) => (
          <div className={styles.skeletonRow} key={item}>
            <span className={styles.skeletonCheck} />
            <span className={styles.skeletonTitle} />
            <span className={styles.skeletonBadge} />
          </div>
        ))}
      </div>
    </div>
  );
}
