"use client";

import { useCallback, useEffect, useMemo, useState } from "react";
import DashboardHeader from "../components/dashboard/DashboardHeader";
import MobileAddButton from "../components/dashboard/MobileAddButton";
import ProgressCard from "../components/dashboard/ProgressCard";
import StatisticsPanel from "../components/dashboard/StatisticsPanel";
import TaskList from "../components/dashboard/TaskList";
import TaskToolbar from "../components/dashboard/TaskToolbar";
import DashboardLayout from "../components/layout/DashboardLayout";
import TaskModal from "../components/modal/TaskModal";
import StatisticsError from "../components/states/StatisticsError";
import {
  createTask,
  deleteTask,
  getDashboard,
  getTasks,
  toggleTask,
  updateTask,
} from "../lib/taskApi";

const STATISTICS_UNAVAILABLE_MESSAGE =
  "Statistics are temporarily unavailable. Your tasks can still be managed.";

function getTodayString() {
  const now = new Date();
  const year = now.getFullYear();
  const month = String(now.getMonth() + 1).padStart(2, "0");
  const day = String(now.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
}

function formatShortDate(dateValue) {
  if (!dateValue) {
    return "No due date";
  }

  const [year, month, day] = dateValue.split("-").map(Number);
  if (!year || !month || !day) {
    return dateValue;
  }

  return new Intl.DateTimeFormat("en-US", {
    month: "short",
    day: "numeric",
    timeZone: "UTC",
  }).format(new Date(Date.UTC(year, month - 1, day)));
}

function formatDueLabel(dateValue, isCompleted) {
  if (isCompleted) {
    return "Completed";
  }

  if (!dateValue) {
    return "No due date";
  }

  const today = getTodayString();
  if (dateValue === today) {
    return "Due today";
  }

  if (dateValue < today) {
    return `Overdue ${formatShortDate(dateValue)}`;
  }

  return `Due ${formatShortDate(dateValue)}`;
}

function normalizeTask(task) {
  return {
    id: task.id,
    title: task.title ?? "",
    priority: task.priority ?? "Medium",
    dueDate: task.dueDate ?? "",
    dueLabel: formatDueLabel(task.dueDate, task.isCompleted),
    category: "Task",
    isCompleted: Boolean(task.isCompleted),
  };
}

function toTaskRequest(formValues) {
  return {
    title: formValues.title.trim(),
    priority: formValues.priority,
    dueDate: formValues.dueDate,
  };
}

function getMutationMessage(error, fallbackMessage) {
  if (error?.message && error.message !== "Failed to fetch") {
    return error.message;
  }

  return fallbackMessage;
}

function createStatisticCards(dashboardData) {
  return [
    {
      label: "Total Tasks",
      value: dashboardData?.totalTasks ?? 0,
      description: "Loaded from Task API",
      tone: "primary",
    },
    {
      label: "Open Tasks",
      value: dashboardData?.openTasks ?? 0,
      description: "Tasks still active",
      tone: "warning",
    },
    {
      label: "Completed",
      value: dashboardData?.completedTasks ?? 0,
      description: `${Math.round(dashboardData?.completionPercentage ?? 0)}% completion rate`,
      tone: "success",
    },
    {
      label: "Overdue",
      value: dashboardData?.overdueTasks ?? 0,
      description:
        (dashboardData?.overdueTasks ?? 0) === 0
          ? "Great job. No delays."
          : "Needs attention",
      tone: (dashboardData?.overdueTasks ?? 0) === 0 ? "success" : "error",
    },
  ];
}

export default function Home() {
  const [tasks, setTasks] = useState([]);
  const [tasksLoading, setTasksLoading] = useState(true);
  const [tasksError, setTasksError] = useState("");
  const [mutationError, setMutationError] = useState("");

  const [dashboardData, setDashboardData] = useState(null);
  const [dashboardLoading, setDashboardLoading] = useState(true);
  const [dashboardError, setDashboardError] = useState("");
  const [selectedStrategy, setSelectedStrategy] = useState("basic");

  const [searchText, setSearchText] = useState("");
  const [debouncedSearchText, setDebouncedSearchText] = useState("");
  const [selectedStatus, setSelectedStatus] = useState("all");

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMode, setModalMode] = useState("create");
  const [selectedTask, setSelectedTask] = useState(null);
  const [modalSubmitting, setModalSubmitting] = useState(false);
  const [modalError, setModalError] = useState("");

  const [togglingTaskId, setTogglingTaskId] = useState(null);
  const [deletingTaskId, setDeletingTaskId] = useState(null);

  useEffect(() => {
    const timeoutId = window.setTimeout(() => {
      setDebouncedSearchText(searchText);
    }, 300);

    return () => {
      window.clearTimeout(timeoutId);
    };
  }, [searchText]);

  const loadTasks = useCallback(async () => {
    setTasksLoading(true);

    try {
      const loadedTasks = await getTasks({
        status: selectedStatus,
        search: debouncedSearchText,
      });
      setTasks(Array.isArray(loadedTasks) ? loadedTasks.map(normalizeTask) : []);
      // Clear any stale error only after a successful response so the error
      // banner does not flicker away during background refreshes.
      setTasksError("");
    } catch (error) {
      console.error("Failed to load tasks.", error);
      setTasksError("Tasks could not be loaded. Check that the Task API is running.");
    } finally {
      setTasksLoading(false);
    }
  }, [debouncedSearchText, selectedStatus]);

  const loadDashboard = useCallback(async () => {
    setDashboardLoading(true);

    try {
      const statistics = await getDashboard(selectedStrategy);
      setDashboardData(statistics);
      // Clear any stale error only after a successful response so the
      // StatisticsError banner stays visible during background refreshes
      // while the Analytics API remains unavailable.
      setDashboardError("");
    } catch (error) {
      console.error("Failed to load dashboard statistics.", error);
      setDashboardError(STATISTICS_UNAVAILABLE_MESSAGE);
    } finally {
      setDashboardLoading(false);
    }
  }, [selectedStrategy]);

  const refreshAfterMutation = useCallback(async () => {
    await Promise.all([loadTasks(), loadDashboard()]);
  }, [loadDashboard, loadTasks]);

  useEffect(() => {
    const timeoutId = window.setTimeout(() => {
      loadTasks();
    }, 0);

    return () => {
      window.clearTimeout(timeoutId);
    };
  }, [loadTasks]);

  useEffect(() => {
    const timeoutId = window.setTimeout(() => {
      loadDashboard();
    }, 0);

    return () => {
      window.clearTimeout(timeoutId);
    };
  }, [loadDashboard]);

  const statisticCards = useMemo(
    () => createStatisticCards(dashboardData),
    [dashboardData],
  );

  function openCreateModal() {
    setModalMode("create");
    setSelectedTask(null);
    setModalError("");
    setIsModalOpen(true);
  }

  function openEditModal(task) {
    setModalMode("edit");
    setSelectedTask(task);
    setModalError("");
    setIsModalOpen(true);
  }

  function closeModal() {
    if (modalSubmitting) {
      return;
    }

    setIsModalOpen(false);
    setSelectedTask(null);
    setModalError("");
  }

  async function handleModalSubmit(formValues) {
    setModalSubmitting(true);
    setModalError("");
    setMutationError("");

    try {
      const request = toTaskRequest(formValues);

      if (modalMode === "edit" && selectedTask) {
        await updateTask(selectedTask.id, request);
      } else {
        await createTask(request);
      }

      setIsModalOpen(false);
      setSelectedTask(null);
      await refreshAfterMutation();
    } catch (error) {
      console.error("Task modal submission failed.", error);
      setModalError(
        getMutationMessage(
          error,
          modalMode === "edit"
            ? "Task could not be updated."
            : "Task could not be created.",
        ),
      );
    } finally {
      setModalSubmitting(false);
    }
  }

  async function handleToggleTask(taskId) {
    if (togglingTaskId !== null) {
      return;
    }

    setTogglingTaskId(taskId);
    setMutationError("");

    try {
      await toggleTask(taskId);
      await refreshAfterMutation();
    } catch (error) {
      console.error("Task completion toggle failed.", error);
      setMutationError(getMutationMessage(error, "Task could not be toggled."));
    } finally {
      setTogglingTaskId(null);
    }
  }

  async function handleDeleteTask(taskId) {
    if (deletingTaskId !== null) {
      return;
    }

    const shouldDelete = window.confirm("Delete this task?");
    if (!shouldDelete) {
      return;
    }

    setDeletingTaskId(taskId);
    setMutationError("");

    try {
      await deleteTask(taskId);
      await refreshAfterMutation();
    } catch (error) {
      console.error("Task deletion failed.", error);
      setMutationError(getMutationMessage(error, "Task could not be deleted."));
    } finally {
      setDeletingTaskId(null);
    }
  }

  const completionPercentage = Math.round(
    dashboardData?.completionPercentage ?? 0,
  );
  const weightedScore =
    dashboardData?.strategy?.toLowerCase() === "weighted"
      ? dashboardData.weightedOpenScore
      : undefined;

  return (
    <DashboardLayout searchValue={searchText} onSearchChange={setSearchText}>
      <DashboardHeader
        title="My Tasks"
        subtitle="Manage your daily tasks and track your progress"
        onAddTask={openCreateModal}
      />

      {dashboardError ? (
        <StatisticsError message={dashboardError} onRetry={loadDashboard} />
      ) : null}

      <StatisticsPanel
        statistics={statisticCards}
        isLoading={dashboardLoading && !dashboardData && !dashboardError}
        hasError={Boolean(dashboardError)}
      />

      <ProgressCard
        label="Workspace Completion"
        percentage={completionPercentage}
        caption={
          selectedStrategy === "weighted"
            ? "Weighted strategy includes the priority load of open tasks."
            : "Basic strategy shows completion based on task status."
        }
        strategy={selectedStrategy}
        onStrategyChange={setSelectedStrategy}
        weightedScore={weightedScore}
        isLoading={dashboardLoading}
      />

      <TaskToolbar
        activeStatus={selectedStatus}
        onStatusChange={setSelectedStatus}
      />

      <TaskList
        tasks={tasks}
        isLoading={tasksLoading}
        errorMessage={tasksError}
        mutationError={mutationError}
        onCreateTask={openCreateModal}
        onRetry={loadTasks}
        onToggleTask={handleToggleTask}
        onEditTask={openEditModal}
        onDeleteTask={handleDeleteTask}
        togglingTaskId={togglingTaskId}
        deletingTaskId={deletingTaskId}
      />

      <MobileAddButton onClick={openCreateModal} />

      {isModalOpen ? (
        <TaskModal
          key={`${modalMode}-${selectedTask?.id ?? "new"}`}
          isOpen={isModalOpen}
          mode={modalMode}
          task={selectedTask}
          onClose={closeModal}
          onSubmit={handleModalSubmit}
          isSubmitting={modalSubmitting}
          serverError={modalError}
        />
      ) : null}
    </DashboardLayout>
  );
}
