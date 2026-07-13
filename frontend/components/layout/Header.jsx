import Image from "next/image";
import styles from "../../styles/layout/Header.module.css";

export default function Header({ searchValue, onSearchChange }) {
  return (
    <header className={styles.header}>
      <a className={styles.skipLink} href="#main-content">
        Skip to content
      </a>
      <div className={styles.mobileBrand}>
        <Image
          src="/taskflow-logo.png"
          alt=""
          width={48}
          height={48}
          className={styles.logo}
          priority
        />
        <span>TaskFlow</span>
      </div>

      <label className={styles.search}>
        <span className={styles.searchLabel}>Search tasks</span>
        <input
          type="search"
          value={searchValue}
          onChange={(event) => onSearchChange(event.target.value)}
          placeholder="Search tasks..."
          aria-label="Search tasks"
        />
      </label>

      <div className={styles.actions} aria-label="Header actions">
        <button className={styles.iconButton} type="button" aria-label="Notifications">
          !
        </button>
        <div className={styles.userIndicator} aria-label="Current user">
          AM
        </div>
      </div>
    </header>
  );
}
