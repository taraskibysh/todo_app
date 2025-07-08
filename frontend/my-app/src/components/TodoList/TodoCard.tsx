import {
    Paper,
    Typography,
    Button,
    Select,
    MenuItem,
    Box,
    IconButton, Modal
} from '@mui/material';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import DeleteIcon from '@mui/icons-material/Delete';
import type { Todo } from '../../features/todos/Todo.ts';
import {useDispatch} from "react-redux";
import type {AppDispatch} from "../../store/Store.ts";
import { updateTodo, setSelectedTodo } from '../../features/todos/todosSlice.ts';
import {useState} from "react";
import {DeleteConfirmForm} from "../Forms/DeleteConfirmForm.tsx";

interface TaskCardProps {
    todo: Todo;
}

const TodoCard = ({ todo }: TaskCardProps) => {
    const dispatch = useDispatch<AppDispatch>()
    const [modalOpen, setModalOpen] = useState(false);

    const updateHandle = (status: number) => {


        const updatedTodo = { ...todo, status };
        dispatch(updateTodo(updatedTodo));
    }

    const selectHandle = (todo : Todo) => {
        dispatch(setSelectedTodo(todo));
    }


    return (
        <Paper
            sx={{
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center',
                p: 2,
                mb: 2,
                borderRadius: 3,
                backgroundColor: '#777a82',
                border: '2px solid #9575CD'
            }}
        >
            <Typography sx={{ fontFamily: 'Comic Sans MS', wordBreak: 'break-word',  whiteSpace: 'normal', overflowWrap: 'break-word' }}>
                {todo.title}
            </Typography>

            <Box sx={{ display: 'flex', alignItems: 'center'  }} >
                <Select
                    value={todo.status}
                    size="small"
                    data-testid="my-select"
                    sx={{
                        backgroundColor: 'white',
                        mr: 1,
                        minWidth: 150,
                        fontSize: '0.9rem',
                    }}
                    onChange={(e) => updateHandle(Number(e.target.value))}
                >
                    <MenuItem value={0}>Done</MenuItem>
                    <MenuItem value={1}>In Progress</MenuItem>
                    <MenuItem value={2}>Todo</MenuItem>
                </Select >

                <Button
                    data-testid="arrow-button"
                    variant="contained"
                    sx={{
                        minWidth: 40,
                        height: 40,
                        borderRadius: 2,
                        backgroundColor: 'white',
                        color: '#7e57c2',
                        mr: 1,
                        '&:hover': { backgroundColor: '#ede7f6' },
                    }}
                    onClick = {() => selectHandle(todo)}
                >
                    <ArrowForwardIcon />
                </Button>

                <IconButton
                    color="error"
                    onClick={() => setModalOpen(true)}>

                    <DeleteIcon fontSize="small" />
                </IconButton>
            </Box>

            <Modal open={modalOpen} onClose={() => setModalOpen(false)} aria-labelledby="modal-create-todo">
                <DeleteConfirmForm id={todo.id} onClose={() => setModalOpen(false)}></DeleteConfirmForm>
            </Modal>
        </Paper>
    );
};

export default TodoCard;
