import React, { useState } from 'react';
import TodoList from './TodoList/TodoList.tsx';
import TodoBoard from './TodoBoard/TodoBoard.tsx';
import {CreateForm} from './Forms/CreateForm'
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { Box } from '@mui/material';

import {Button, Modal} from "@mui/material";

const TodoApp: React.FC = () => {
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    return (
        <div style={{ height: '100vh', padding: 0, margin: 0 }}>
            <Button style = {{marginLeft: 0}} variant="contained" color="secondary" onClick={handleOpen}>
                Create Task
            </Button>
            <br/>
            <br/>

            <Modal open={open} onClose={handleClose} aria-labelledby="modal-create-todo">
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <CreateForm onClose={handleClose} />
                </LocalizationProvider>
            </Modal>


            <Box
                sx={{
                    display: 'flex',
                    width: '80vw',
                    height: '80vh',
                    gap: '16px'
                }}
            >
                <Box
                    sx={{
                        flex: 1,
                        overflowX: 'hidden',
                        overflowY: 'auto'
                    }}
                >
                    <TodoList />
                </Box>
                <Box
                    sx={{
                        flex: 1,
                        overflowY: 'auto',
                        borderRight: '1px solid #ddd',
                    }}
                >
                    <TodoBoard />
                </Box>
            </Box>

        </div>
    );
};

export default TodoApp;