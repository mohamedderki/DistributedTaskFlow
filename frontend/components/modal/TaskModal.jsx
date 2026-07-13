"use client";

import { useEffect, useId, useState } from "react";
import styles from "../../styles/modal/TaskModal.module.css";

function getDefaultDueDate() {
  const now = new Date();
  const year = now.getFullYear();
  const month = String(now.getMonth() + 1).padStart(2, "0");
  const day = String(now.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
}

function getEmptyTask() {
  return {
    title: "",
    priority: "Medium",
    dueDate: getDefaultDueDate(),
  };
}

function getInitialValues(task) {
  return task
    ? {
        title: task.title,
        priority: task.priority,
        dueDate: task.dueDate,
      }
    : getEmptyTask();
}

export default function TaskModal({
  isOpen = true,
  mode = "create",
  task,
  onClose,
  onSubmit,
  isSubmitting = false,
  serverError = "",
}) {
  const titleId = useId();
  const descriptionId = useId();
  const [formValues, setFormValues] = useState(() => getInitialValues(task));
  const [error, setError] = useState("");

  useEffect(() => {
    if (!isOpen) {
      return undefined;
    }

    function handleKeyDown(event) {
      if (event.key === "Escape") {
        onClose();
      }
    }

    document.addEventListener("keydown", handleKeyDown);

    return () => {
      document.removeEventListener("keydown", handleKeyDown);
    };
  }, [isOpen, onClose]);

  if (!isOpen) {
    return null;
  }

  function handleChange(event) {
    const { name, value } = event.target;

    setFormValues((currentValues) => ({
      ...currentValues,
      [name]: value,
    }));
  }

  function handleSubmit(event) {
    event.preventDefault();

    if (isSubmitting) {
      return;
    }

    if (formValues.title.trim().length === 0) {
      setError("Enter a task title before saving.");
      return;
    }

    if (!formValues.dueDate) {
      setError("Choose a due date before saving.");
      return;
    }

    setError("");
    onSubmit({
      ...formValues,
      title: formValues.title.trim(),
    });
  }

  function handleOverlayClick(event) {
    if (event.target === event.currentTarget) {
      onClose();
    }
  }

  return (
    <div className={styles.overlay} onMouseDown={handleOverlayClick}>
      <section
        className={styles.dialog}
        role="dialog"
        aria-modal="true"
        aria-labelledby={titleId}
        aria-describedby={descriptionId}
      >
        <div className={styles.dialogHeader}>
          <div>
            <h2 id={titleId}>{mode === "edit" ? "Edit Task" : "Create New Task"}</h2>
            <p id={descriptionId}>Add details for your upcoming assignment.</p>
          </div>
          <button
            className={styles.closeButton}
            type="button"
            aria-label="Close task dialog"
            onClick={onClose}
            disabled={isSubmitting}
          >
            x
          </button>
        </div>

        <form className={styles.form} onSubmit={handleSubmit}>
          <label className={styles.field}>
            <span>Task title</span>
            <input
              name="title"
              type="text"
              value={formValues.title}
              onChange={handleChange}
              maxLength={120}
              placeholder="e.g. Update brand guidelines"
              disabled={isSubmitting}
            />
          </label>

          <div className={styles.inlineFields}>
            <label className={styles.field}>
              <span>Priority</span>
              <select
                name="priority"
                value={formValues.priority}
                onChange={handleChange}
                disabled={isSubmitting}
              >
                <option value="Low">Low</option>
                <option value="Medium">Medium</option>
                <option value="High">High</option>
              </select>
            </label>

            <label className={styles.field}>
              <span>Due date</span>
              <input
                name="dueDate"
                type="date"
                value={formValues.dueDate}
                onChange={handleChange}
                disabled={isSubmitting}
              />
            </label>
          </div>

          {error ? <p className={styles.error}>{error}</p> : null}
          {serverError ? <p className={styles.error}>{serverError}</p> : null}

          <div className={styles.actions}>
            <button
              className={styles.secondaryButton}
              type="button"
              onClick={onClose}
              disabled={isSubmitting}
            >
              Cancel
            </button>
            <button className={styles.primaryButton} type="submit" disabled={isSubmitting}>
              {isSubmitting ? "Saving..." : "Save Task"}
            </button>
          </div>
        </form>
      </section>
    </div>
  );
}
