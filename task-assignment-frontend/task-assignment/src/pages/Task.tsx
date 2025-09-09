import PendingTask from "../components/PendingTask";
import CompletedTask from "../components/CompletedTask";
import type { TaskListQuery as TaskListQueryType } from "../query/__generated__/TaskListQuery.graphql";
import { useLazyLoadQuery } from "react-relay";
import { TaskListQuery } from "../query/TaskListQuery";
import AddTask from "../components/AddTask";
const TaskBoard = () => {

  const data = useLazyLoadQuery<TaskListQueryType>(
    TaskListQuery,
    {},
    { fetchPolicy: "store-or-network" }
  );

  const pendingTasks: any = data.getAllTasks.filter(task => task.status === "PENDING");
  const completedTasks:any = data.getAllTasks.filter(task => task.status === "COMPLETED");


  return (
    <div className="task-board">
      <div className="task-column">
        <div className="task-column-title">Pending Tasks</div>
        <PendingTask initialItems={pendingTasks} />
      </div>
      <div className="task-column">
        <div className="task-column-title">Completed Tasks</div>
        <CompletedTask initialItems={completedTasks} />
      </div>
      <div>
      <AddTask />
      </div>
    </div>
    
  );
};

export default TaskBoard;
