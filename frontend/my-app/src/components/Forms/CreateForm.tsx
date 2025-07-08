import {type FormEvent, useState} from "react";
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import dayjs, { Dayjs } from 'dayjs';
import  "./CreateForm.css"
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import {Button, InputLabel} from '@mui/material';
import type {Todo} from "../../features/todos/Todo";
import {useDispatch} from "react-redux";
import type {AppDispatch} from "../../store/Store.ts";
import { addTodo } from '../../features/todos/todosSlice.ts';


interface Props {
    onClose: () => void;
}

export function CreateForm({onClose}: Props ) {
    const dispatch = useDispatch<AppDispatch>();

    const [selectedDate, setSelectedDate] = useState<Dayjs>(dayjs());
    const [selectedStatus, setSelectedStatus] = useState(0);
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [titleError, setTitleError] = useState(false);


    const handleSubmit = (event: FormEvent) => {
        event.preventDefault();


        if (title.length == 0) {
            setTitleError(true);
            return;
        }
        if(selectedDate < dayjs()) {
            return;
        }

        const newTodo: Todo = {
            title: title.trim(),
            description: description,
            deadline:  selectedDate.toISOString(),
            status: selectedStatus

        }
        console.log("newTodo", newTodo)

        dispatch(addTodo(newTodo));

        onClose();


    }

    return (
        <div className="container" >
            <h1> create form</h1>
            <form>
                <Box mb={2}>
                    <TextField fullWidth label="Title"
                               variant="outlined"
                               value = {title}
                               required
                               error={titleError}
                               onChange={(value) => setTitle(value.target.value)}
                               helperText={`${title.length}/50`}
                               inputProps={{ maxLength: 50 }}


                    />
                </Box>

                <Box mb={2}>
                    <TextField fullWidth label="Description"
                               variant="outlined"
                               multiline rows={3}
                               value = {description}
                               onChange={(e) => setDescription(e.target.value)}
                               helperText={`${description.length}/200`}
                               inputProps={{ maxLength: 200 }}/>
                </Box>
                <Box mb={2}>
                <FormControl fullWidth>
                    <InputLabel id="select-label">Status</InputLabel>
                    <Select
                        labelId="select-label"
                        id="select"
                        value={selectedStatus}
                        onChange={(value) => setSelectedStatus(Number(value.target.value))}
                        label="Age"
                    >
                        <MenuItem value="0">Done</MenuItem>
                        <MenuItem value="1">In Progress</MenuItem>
                        <MenuItem value="2">To do</MenuItem>
                    </Select>
                </FormControl>
                </Box>

                <Box mb={2}>
                    <DateTimePicker
                        label="Due Date"
                        value={selectedDate}
                        onChange={(newValue) => setSelectedDate(dayjs(newValue))}
                        slotProps={{ textField: { fullWidth: true } }}
                        ampm={false}
                        minDateTime={dayjs()}
                    />
                </Box>
                <Button sx={{backgroundColor: '#743acc', marginRight: 3 }} variant="contained" onClick={onClose}>Close</Button>
                <Button sx={{
                    backgroundColor: '#743acc', marginLeft: 3 }} variant="contained" onClick={handleSubmit} >Submit</Button>

            </form>
        </div>
    )
}