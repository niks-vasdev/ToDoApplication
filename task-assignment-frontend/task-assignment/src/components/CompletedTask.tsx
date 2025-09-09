import { Badge, Flex, Item, ListView, Text, ToastQueue, useDragAndDrop, View } from "@adobe/react-spectrum";
import { useMutation } from "react-relay";
import { UpdateTaskStatusMutation } from "../relay/UpdateTaskStatusMutation";
import type { TaskDto } from "../interfaces/TaskDto";

interface CompletedTaskProps {
  initialItems: TaskDto[];
}

const CompletedTask = ({ initialItems }: CompletedTaskProps) => {
  const [commitUpdateStatus] = useMutation(UpdateTaskStatusMutation);

  const handleDrop = (taskId: string, newStatus: "PENDING" | "COMPLETED") => {
    commitUpdateStatus({
      variables: {
        input: {
          id: taskId,
          status: newStatus,
        },
      },
      onCompleted: () => {
        ToastQueue.positive('Task Marked as Completed', { timeout: 3000 });
      },
      onError: () => {
        ToastQueue.negative('Something went wrong', { timeout: 3000 });
      },
    });
  };
  const completedTasks = initialItems;

  const { dragAndDropHooks } = useDragAndDrop({
    acceptedDragTypes: ["task-app"],
    getItems: (keys) => [...keys].map(key => {
      const task = completedTasks.find(t => t.id === key)!;
      return { "task-app": JSON.stringify(task) };
    }),
    onRootDrop: async (e) => {
      const dropped = await Promise.all(
        e.items.map(async (item: any) => JSON.parse(await item.getText("task-app")))
      );

      dropped.forEach(task => {
        handleDrop(task.id, "COMPLETED");
      });
    }
  });

  return (
    <div>
      <ListView
        aria-label="Completed tasks"
        UNSAFE_className="list-view"
        height="70vh"
        selectionMode="multiple"
        items={completedTasks.map(task => ({ ...task, key: task.id }))}
        dragAndDropHooks={dragAndDropHooks}
      >
        {(item: TaskDto) => (
          <Item key={item.id} textValue={item.title}>
            <Flex direction="column" gap="size-100">
              <Text>Title: {item.title}</Text>
              <Text>Description: {item.description || "No description provided"}</Text>
              <View
                height="single-line-height"
                flex= "1">
                <Text >Status: <Badge UNSAFE_className="badge-style" variant="positive">{item.status}</Badge></Text>
              </View>
              <Text>Created: {new Date(item.createdUtc).toLocaleString()}</Text>
              <Text>
                Completed:{" "}
                {item.completedUtc
                  ? new Date(item.completedUtc).toLocaleString()
                  : "Not completed"}
              </Text>
            </Flex>
          </Item>
        )}

      </ListView>
    </div>
  );
};

export default CompletedTask;
