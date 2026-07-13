import Header from "./Header";
import Sidebar from "./Sidebar";
import styles from "../../styles/layout/DashboardLayout.module.css";

export default function DashboardLayout({ children, searchValue, onSearchChange }) {
  return (
    <div className={styles.shell}>
      <Sidebar />
      <div className={styles.workspace}>
        <Header searchValue={searchValue} onSearchChange={onSearchChange} />
        <main id="main-content" className={styles.main}>
          <div className={styles.content}>{children}</div>
        </main>
      </div>
    </div>
  );
}
