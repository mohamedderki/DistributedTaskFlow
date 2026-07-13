import styles from "../../styles/dashboard/DashboardHeader.module.css";

export default function DashboardHeader({ title, subtitle, onAddTask }) {
  return (
    <section className={styles.header} aria-labelledby="dashboard-title">
      <div>
        <p className={styles.eyebrow}>Task workspace</p>
        <h1 id="dashboard-title">{title}</h1>
        <p className={styles.subtitle}>{subtitle}</p>
      </div>

      <button className={styles.addButton} type="button" onClick={onAddTask}>
        <span aria-hidden="true">+</span>
        Add Task
      </button>
    </section>
  );
}
