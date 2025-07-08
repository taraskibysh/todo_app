import React, {useState} from "react";
import type {Step} from "../../features/steps/Step.ts";
import {Checkbox, IconButton, TextField} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import {useDispatch} from "react-redux";
import type {  AppDispatch } from '../../store/Store.ts';
import {addStep, deleteStep} from "../../features/todos/todosSlice.ts";

export const StepsList: React.FC<{id: number, steps: Step[]}> = ({id, steps }) => {

    const [title, setTitle] = useState("");
    const dispatch = useDispatch<AppDispatch>();

    const createHandle = (title: string) => {
        const newStep: Step = {
            title: title,
            isDone: false
        }


        dispatch(addStep({todoId:id, step: newStep}));
        setTitle("");


    }

    const handleDeleteStep = (stepId: number) => {
        dispatch(deleteStep({ todoId: id!, id: stepId }));
    };

    return (
        <ul style={{margin: 'auto'}}>
            {steps.map(step => (
                <li key={step.id}>
                    <Checkbox
                        checked={false}
                        color="secondary"
                        onChange={() => handleDeleteStep(step.id!)}
                    />
                    {step.title}
                </li>

            ))}

            <div style={{display: "flex", gap: "10px"}}>
                <TextField value = {title}
                           onChange={(e) => setTitle(e.target.value)}
                           id="standard-basic" label="New step" variant="standard" helperText={`${title.length}/30`}
                           inputProps={{ maxLength: 30 }}/>
                <IconButton color="secondary" onClick={() => {if (title.trim() !== "") {
                    createHandle(title);
                }}}>
                    <AddIcon/>
                </IconButton>
            </div>
        </ul>
    )
}
