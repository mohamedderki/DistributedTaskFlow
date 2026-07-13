import styles from "../../styles/dashboard/ProgressCard.module.css";

export default function ProgressCard({
  label,
  percentage,
  caption,
  strategy,
  onStrategyChange,
  weightedScore,
  isLoading = false,
}) {
  const safePercentage = Number.isFinite(percentage)
    ? Math.max(0, Math.min(100, Math.round(percentage)))
    : 0;

  return (
    <section className={styles.card} aria-labelledby="workspace-progress-title">
      <div>
        <h2 id="workspace-progress-title">{label}</h2>
        <p>{caption}</p>
        {typeof weightedScore === "number" ? (
          <p className={styles.score}>Weighted open score: {weightedScore}</p>
        ) : null}
      </div>

      <div className={styles.progressSummary}>
        <label className={styles.strategyField}>
          <span>Strategy</span>
          <select
            value={strategy}
            onChange={(event) => onStrategyChange(event.target.value)}
            disabled={isLoading}
          >
            <option value="basic">Basic</option>
            <option value="weighted">Weighted</option>
          </select>
        </label>
        <span>{isLoading ? "..." : `${safePercentage}%`}</span>
        <progress
          className={styles.progressMeter}
          aria-label={`${safePercentage}% complete`}
          max="100"
          value={safePercentage}
        />
        <p>Monthly Goal</p>
      </div>
    </section>
  );
}
