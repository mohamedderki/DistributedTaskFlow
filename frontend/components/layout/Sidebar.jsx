import Image from "next/image";
import styles from "../../styles/layout/Sidebar.module.css";

const navItems = [
  { id: "dashboard", label: "Dashboard" },
  { id: "my-tasks", label: "My Tasks" },
];

export default function Sidebar() {
  return (
    <aside className={styles.sidebar} aria-label="Primary navigation">
      <div className={styles.brand}>
        <Image
          src="/taskflow-logo.png"
          alt=""
          width={72}
          height={72}
          className={styles.logo}
          priority
        />
        <div>
          <p className={styles.brandName}>TaskFlow Pro</p>
          <p className={styles.brandSubline}>Personal Workspace</p>
        </div>
      </div>

      <nav className={styles.nav} aria-label="TaskFlow navigation">
        {navItems.map((item) => {
          const isActive = item.id === "my-tasks";

          return (
            <button
              className={`${styles.navItem} ${isActive ? styles.active : ""}`}
              type="button"
              aria-current={isActive ? "page" : undefined}
              key={item.id}
            >
              <span className={styles.navIcon} aria-hidden="true">
                {isActive ? ">" : "-"}
              </span>
              {item.label}
            </button>
          );
        })}
      </nav>

      <div className={styles.footer}>
        <p>Ready for API data</p>
      </div>
    </aside>
  );
}
