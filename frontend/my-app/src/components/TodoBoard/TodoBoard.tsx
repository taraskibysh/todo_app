import React, { useState } from "react";
import "./TodoBoard.css";
import { useSelector} from "react-redux";
import type { RootState } from '../../store/Store.ts';
import { Button, Modal, Divider } from "@mui/material";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import {DeadlineCalendar} from "./DeadlineCalendar.tsx";
import {StepsList} from "./StepList.tsx";
import { EditForm } from "../Forms/EditForm.tsx";

const getStatusLabel = (status?: number) => {
    switch (status) {
        case 0: return "Done";
        case 1: return "In Progress";
        case 2: return "To do";
        default: return "Unknown";
    }
};



const TodoBoard: React.FC = () => {
    const selectedTodo = useSelector((state: RootState) => state.todos.selectedItem);
    const [modalOpen, setModalOpen] = useState(false);

    if (!selectedTodo) {
        return (
            <div className="todo-board">
                <h1 style={{ textAlign: "center", fontSize: '3rem' }}>choose a task : )</h1>
            </div>
        );
    }



    return (
        <div className="todo-board">
            <h1 style={{ fontSize: "250%" }}>{selectedTodo.title}</h1>

            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div style={{ textAlign: 'left', flex: 1 }}>
                    <h2>Description</h2>
                    <p>{selectedTodo.description || 'No description'}</p>
                </div>

                <div style={{ textAlign: 'right', minWidth: 150 }}>
                    <h2>Status</h2>
                    <p>{getStatusLabel(selectedTodo.status)}</p>
                </div>
            </div>

            <Divider />
            <br/>
            <div style={{ display: 'flex', alignItems: 'center' }}>
                <DeadlineCalendar deadline={selectedTodo.deadline} />

                <div style={{ display: 'flex', gap: "10px", flexDirection: 'column', margin: 'auto' }}>
                    <h2>Task steps</h2>
                    <StepsList steps={selectedTodo.steps ? selectedTodo.steps : []} id = {selectedTodo.id!} />
                </div>
            </div>

            <Button variant="contained" color="secondary" onClick={() => setModalOpen(true)}>Edit</Button>

            <Modal open={modalOpen} onClose={() => setModalOpen(false)} aria-labelledby="modal-edit-todo">
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <EditForm onClose={() => setModalOpen(false)} value={selectedTodo} />
                </LocalizationProvider>
            </Modal>
        </div>
    );
};

export default TodoBoard;
