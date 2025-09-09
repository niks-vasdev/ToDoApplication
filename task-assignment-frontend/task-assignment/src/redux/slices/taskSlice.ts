// taskSlice.ts
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { TaskDto } from "../../interfaces/TaskDto";

interface TaskState {
    pendingTasks: TaskDto[];
    completedTasks: TaskDto[];
}

const initialState: TaskState = {
    pendingTasks: [],
    completedTasks: []
};

const taskSlice = createSlice({
    name: "task",
    initialState,
    reducers: {
        setTasks: (state, action: PayloadAction<TaskDto[]>) => {
            state.pendingTasks = action.payload.filter(t => t.status === "PENDING");
            state.completedTasks = action.payload.filter(t => t.status === "COMPLETED");
        },
        addTask: (state, action: PayloadAction<TaskDto>) => {
            if (action.payload.status === "PENDING") state.pendingTasks.push(action.payload);
            else state.completedTasks.push(action.payload);
        },
        deleteTask: (state, action: PayloadAction<{ id: string; status: "PENDING" | "COMPLETED" }>) => {
            if (action.payload.status === "PENDING")
                state.pendingTasks = state.pendingTasks.filter(t => t.id !== action.payload.id);
            else
                state.completedTasks = state.completedTasks.filter(t => t.id !== action.payload.id);
        }
    }
});

export const { setTasks, addTask, deleteTask } = taskSlice.actions;
export default taskSlice.reducer;
