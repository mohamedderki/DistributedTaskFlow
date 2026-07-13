import styles from "../../styles/dashboard/MobileAddButton.module.css";

export default function MobileAddButton({ onClick }) {
  return (
    <button
      className={styles.button}
      type="button"
      onClick={onClick}
      aria-label="Add task"
    >
      +
    </button>
  );
}
