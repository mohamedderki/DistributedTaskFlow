import StatisticCard from "./StatisticCard";
import styles from "../../styles/dashboard/StatisticsPanel.module.css";

const loadingCards = ["Total Tasks", "Open Tasks", "Completed", "Overdue"];

export default function StatisticsPanel({
  statistics,
  isLoading = false,
  hasError = false,
  isDisabled = false,
}) {
  return (
    <section className={styles.panel} aria-label="Task statistics">
      {isLoading
        ? loadingCards.map((label) => (
            <StatisticCard key={label} label={label} isLoading />
          ))
        : statistics.map((item) => (
            <StatisticCard
              key={item.label}
              label={item.label}
              value={item.value}
              description={item.description}
              tone={item.tone}
              isDisabled={isDisabled || hasError}
            />
          ))}
    </section>
  );
}
