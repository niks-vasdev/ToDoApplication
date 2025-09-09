import { useState } from "react";
import { TextField, TextArea, Button, Flex, ToastQueue } from "@adobe/react-spectrum";
import { useMutation } from "react-relay";
import { AddTaskMutation } from "../relay/AddTaskMutation";

const AddTask = () => {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [commitAddTask, isInFlight] = useMutation(AddTaskMutation);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!title.trim()) return;

        commitAddTask({
            variables: {
                input: { title, description },
            },

            updater: (store) => {
                const newTask = store.getRootField("createTask");
                if (!newTask) return;

                const root = store.getRoot();
                const existingTasks = root.getLinkedRecords("getAllTasks") || [];

                root.setLinkedRecords([...existingTasks, newTask], "getAllTasks");
            },
            onCompleted: () => {
                ToastQueue.positive('Task Added Successfully', { timeout: 3000 });
                setTitle("");
                setDescription("");
            },

            onError: () => {
                ToastQueue.negative('Something went wrong', { timeout: 3000 });
            },
        });
    };

    return (
        <div className="add-task-container">
            <h2 className="task-heading">Add New Task</h2>
            <form onSubmit={handleSubmit}>
                <Flex direction="column" gap="size-200" width="size-3600">
                    <Flex direction="column" gap="size-200" width="100%">
                        <TextField
                            label="Task Title"
                            value={title}
                            onChange={setTitle}
                            isRequired
                            placeholder="Enter task title"
                            width="100%"    
                        />
                        <TextArea
                            label="Task Description"
                            value={description}
                            onChange={setDescription}
                            placeholder="Enter task description"
                            width="100%"   
                        />
                    </Flex>

                    <Button
                        type="submit"
                        variant="cta"
                        isDisabled={isInFlight || !title.trim()}
                    >
                        Add Task
                    </Button>
                </Flex>
            </form>
        </div>

    );
};

export default AddTask;
