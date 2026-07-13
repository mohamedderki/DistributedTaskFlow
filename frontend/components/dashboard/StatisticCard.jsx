import styles from "../../styles/dashboard/StatisticCard.module.css";

export default function StatisticCard({
  label,
  value,
  description,
  tone = "primary",
  isLoading = false,
  isDisabled = false,
}) {
  const cardClassName = [
    styles.card,
    styles[tone],
    isDisabled ? styles.disabled : "",
  ]
    .filter(Boolean)
    .join(" ");

  if (isLoading) {
    return (
      <article className={`${styles.card} ${styles.loading}`} aria-label={`${label} loading`}>
        <div className={styles.skeletonLabel} />
        <div className={styles.skeletonValue} />
        <div className={styles.skeletonText} />
      </article>
    );
  }

  return (
    <article className={cardClassName}>
      <div className={styles.cardTopline}>
        <h2>{label}</h2>
        <span className={styles.dot} aria-hidden="true" />
      </div>
      <p className={styles.value}>{value}</p>
      {description ? <p className={styles.description}>{description}</p> : null}
    </article>
  );
}
